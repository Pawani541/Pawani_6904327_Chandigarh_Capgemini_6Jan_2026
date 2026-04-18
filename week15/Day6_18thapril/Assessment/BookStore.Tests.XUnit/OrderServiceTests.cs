using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Moq;
using Xunit;

namespace BookStore.Tests.XUnit
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepo;
        private readonly Mock<IBookRepository> _mockBookRepo;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepo = new Mock<IOrderRepository>();
            _mockBookRepo = new Mock<IBookRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockMapper = new Mock<IMapper>();

            _orderService = new OrderService(
                _mockOrderRepo.Object,
                _mockBookRepo.Object,
                _mockEmailService.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task GetOrderByIdAsync_WithValidId_ShouldReturnOrder()
        {
            var order = new Order { OrderId = 1, UserId = 1, TotalAmount = 499, Status = "Pending" };
            var orderDto = new OrderResponseDto { OrderId = 1, TotalAmount = 499, Status = "Pending" };

            _mockOrderRepo.Setup(r => r.GetOrderWithItemsAsync(1)).ReturnsAsync(order);
            _mockMapper.Setup(m => m.Map<OrderResponseDto>(order)).Returns(orderDto);

            var result = await _orderService.GetOrderByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
            Assert.Equal(499, result.TotalAmount);
        }

        [Fact]
        public async Task GetOrderByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            _mockOrderRepo.Setup(r => r.GetOrderWithItemsAsync(999)).ReturnsAsync((Order?)null);

            var result = await _orderService.GetOrderByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task PlaceOrderAsync_WithInvalidBook_ShouldThrowException()
        {
            var dto = new OrderCreateDto
            {
                UserId = 1,
                Items = new List<OrderItemCreateDto> { new OrderItemCreateDto { BookId = 999, Qty = 1 } }
            };
            _mockBookRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Book?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.PlaceOrderAsync(dto));
        }

        [Fact]
        public async Task PlaceOrderAsync_WithInsufficientStock_ShouldThrowException()
        {
            var dto = new OrderCreateDto
            {
                UserId = 1,
                Items = new List<OrderItemCreateDto> { new OrderItemCreateDto { BookId = 1, Qty = 100 } }
            };
            var book = new Book { BookId = 1, Title = "Test Book", Stock = 5, Price = 299 };
            _mockBookRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _orderService.PlaceOrderAsync(dto));
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_WithValidId_ShouldUpdateStatus()
        {
            var order = new Order { OrderId = 1, Status = "Pending" };
            _mockOrderRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);
            _mockOrderRepo.Setup(r => r.UpdateAsync(order)).Returns(Task.CompletedTask);
            _mockOrderRepo.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            await _orderService.UpdateOrderStatusAsync(1, "Completed");

            _mockOrderRepo.Verify(r => r.UpdateAsync(It.IsAny<Order>()), Times.Once);
            _mockOrderRepo.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderStatusAsync_WithInvalidId_ShouldThrowException()
        {
            _mockOrderRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Order?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _orderService.UpdateOrderStatusAsync(999, "Completed"));
        }

        [Fact]
        public async Task GetOrdersByUserAsync_ShouldReturnUserOrders()
        {
            var orders = new List<Order>
            {
                new Order { OrderId = 1, UserId = 1, TotalAmount = 499, Status = "Pending" },
                new Order { OrderId = 2, UserId = 1, TotalAmount = 299, Status = "Completed" }
            };
            var orderDtos = new List<OrderResponseDto>
            {
                new OrderResponseDto { OrderId = 1, TotalAmount = 499 },
                new OrderResponseDto { OrderId = 2, TotalAmount = 299 }
            };

            _mockOrderRepo.Setup(r => r.GetOrdersByUserAsync(1)).ReturnsAsync(orders);
            _mockMapper.Setup(m => m.Map<IEnumerable<OrderResponseDto>>(orders)).Returns(orderDtos);

            var result = await _orderService.GetOrdersByUserAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
