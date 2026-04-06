using OrderApi.Repositories;   // 👈 THIS FIXES ERROR
using OrderApi.DTOs;
using OrderApi.Models;
using AutoMapper;
using log4net;

namespace OrderApi.Services;

public interface IOrderService
{
    Task<string> CreateOrder(OrderDto dto);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;
    private static readonly ILog log = LogManager.GetLogger(typeof(OrderService));

    public OrderService(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<string> CreateOrder(OrderDto dto)
    {
        log.Info("Order started");

        var order = _mapper.Map<Order>(dto);

        await _repo.AddOrder(order);

        log.Info("Order saved");

        return "Order Created";
    }
}