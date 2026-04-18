using Asp.Versioning;
using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderCreateDto> _validator;

        public OrdersController(IOrderService orderService, IValidator<OrderCreateDto> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found." });
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUserAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDto dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var order = await _orderService.PlaceOrderAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(new { message = "Status cannot be empty." });

            await _orderService.UpdateOrderStatusAsync(id, status);
            return Ok(new { message = $"Order {id} status updated to {status}." });
        }
    }
}
