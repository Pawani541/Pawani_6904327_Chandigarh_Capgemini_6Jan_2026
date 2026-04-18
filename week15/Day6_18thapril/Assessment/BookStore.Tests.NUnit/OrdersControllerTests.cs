using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.API.Controllers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BookStore.Tests.NUnit
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private Mock<IValidator<OrderCreateDto>> _mockValidator;
        private OrdersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockValidator = new Mock<IValidator<OrderCreateDto>>();
            _controller = new OrdersController(
                _mockOrderService.Object,
                _mockValidator.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturn200WithOrders()
        {
            // Arrange
            var orders = new List<OrderResponseDto>
            {
                new OrderResponseDto { OrderId = 1, TotalAmount = 499, Status = "Pending" },
                new OrderResponseDto { OrderId = 2, TotalAmount = 299, Status = "Completed" }
            };
            _mockOrderService.Setup(s => s.GetAllOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetById_WithValidId_ShouldReturn200()
        {
            // Arrange
            var order = new OrderResponseDto { OrderId = 1, TotalAmount = 499 };
            _mockOrderService.Setup(s => s.GetOrderByIdAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_WithInvalidId_ShouldReturn404()
        {
            // Arrange
            _mockOrderService.Setup(s => s.GetOrderByIdAsync(999))
                             .ReturnsAsync((OrderResponseDto?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task PlaceOrder_WithValidData_ShouldReturn201()
        {
            // Arrange
            var dto = new OrderCreateDto
            {
                UserId = 1,
                Items = new List<OrderItemCreateDto>
                {
                    new OrderItemCreateDto { BookId = 1, Qty = 2 }
                }
            };
            var orderDto = new OrderResponseDto { OrderId = 1, TotalAmount = 598 };

            _mockValidator.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult());
            _mockOrderService.Setup(s => s.PlaceOrderAsync(dto)).ReturnsAsync(orderDto);

            // Act
            var result = await _controller.PlaceOrder(dto);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task PlaceOrder_WithInvalidData_ShouldReturn400()
        {
            // Arrange
            var dto = new OrderCreateDto { UserId = 0, Items = new List<OrderItemCreateDto>() };
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("UserId", "Invalid user."),
                new ValidationFailure("Items", "Order must contain at least one item.")
            };

            _mockValidator.Setup(v => v.ValidateAsync(dto, default))
                .ReturnsAsync(new ValidationResult(failures));

            // Act
            var result = await _controller.PlaceOrder(dto);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetByUser_ShouldReturn200WithUserOrders()
        {
            // Arrange
            var orders = new List<OrderResponseDto>
            {
                new OrderResponseDto { OrderId = 1, TotalAmount = 499 }
            };
            _mockOrderService.Setup(s => s.GetOrdersByUserAsync(1)).ReturnsAsync(orders);

            // Act
            var result = await _controller.GetByUser(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateStatus_WithValidData_ShouldReturn200()
        {
            // Arrange
            _mockOrderService.Setup(s => s.UpdateOrderStatusAsync(1, "Completed"))
                             .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateStatus(1, "Completed");

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task UpdateStatus_WithEmptyStatus_ShouldReturn400()
        {
            // Act
            var result = await _controller.UpdateStatus(1, "");

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}
