using ECommerceOrderManagement.API.DTOs;
using ECommerceOrderManagement.API.Models;
using ECommerceOrderManagement.API.Repositories.Interfaces;
using ECommerceOrderManagement.API.Services.Interfaces;

namespace ECommerceOrderManagement.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICacheService _cache;

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, ICacheService cache)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _cache = cache;
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync() =>
            (await _orderRepo.GetAllAsync()).Select(Map).ToList();

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var o = await _orderRepo.GetByIdAsync(id);
            return o == null ? null : Map(o);
        }

        public async Task<List<OrderResponseDto>> GetOrdersByUserAsync(int userId) =>
            (await _orderRepo.GetByUserIdAsync(userId)).Select(Map).ToList();

        public async Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = "Confirmed",
                OrderItems = new List<OrderItem>()
            };
            decimal total = 0;
            foreach (var item in dto.Items)
            {
                var p = await _productRepo.GetByIdAsync(item.ProductId)
                    ?? throw new Exception($"Product {item.ProductId} not found");
                if (p.StockQuantity < item.Quantity)
                    throw new Exception($"Insufficient stock: {p.Name}");
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = p.Price
                });
                total += p.Price * item.Quantity;
                p.StockQuantity -= item.Quantity;
                await _productRepo.UpdateAsync(p);
            }
            order.TotalAmount = total;
            await _orderRepo.AddAsync(order);
            await _cache.RemoveAsync("all_orders");
            return Map(order);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var o = await _orderRepo.GetByIdAsync(id);
            if (o == null) return false;
            o.Status = status;
            await _orderRepo.UpdateAsync(o);
            return true;
        }

        private static OrderResponseDto Map(Order o) => new()
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            Status = o.Status,
            TotalAmount = o.TotalAmount,
            Items = o.OrderItems.Select(oi => new OrderItemResponseDto
            {
                ProductName = oi.Product?.Name ?? "",
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
    }
}

