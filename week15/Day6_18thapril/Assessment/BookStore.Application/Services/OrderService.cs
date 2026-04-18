using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IBookRepository bookRepository,
            IEmailService emailService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
            return order == null ? null : _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserAsync(userId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto dto)
        {
            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in dto.Items)
            {
                var book = await _bookRepository.GetByIdAsync(item.BookId);
                if (book == null)
                    throw new KeyNotFoundException($"Book with ID {item.BookId} not found.");

                if (book.Stock < item.Qty)
                    throw new InvalidOperationException($"Insufficient stock for book: {book.Title}");

                book.Stock -= item.Qty;
                await _bookRepository.UpdateAsync(book);

                var orderItem = new OrderItem
                {
                    BookId = item.BookId,
                    Qty = item.Qty,
                    Price = book.Price
                };

                total += book.Price * item.Qty;
                orderItems.Add(orderItem);

                if (book.Stock < 5)
                {
                    await _emailService.SendLowStockAlertAsync(
                        "admin@bookstore.com", book.Title, book.Stock);
                }
            }

            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = total,
                Status = "Pending",
                OrderItems = orderItems
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveAsync();

            var user = await _orderRepository.GetUserByIdAsync(dto.UserId);
            if (user != null)
            {
                await _emailService.SendOrderConfirmationAsync(
                    user.Email, user.FullName, order.OrderId, total);
            }

            var savedOrder = await _orderRepository.GetOrderWithItemsAsync(order.OrderId);
            return _mapper.Map<OrderResponseDto>(savedOrder!);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");

            order.Status = status;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveAsync();
        }
    }
}
