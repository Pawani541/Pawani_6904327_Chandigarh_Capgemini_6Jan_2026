using Microsoft.AspNetCore.Mvc;
using OrderAPI.Api.Models;
using OrderAPI.Api.Services;

namespace OrderAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            var result = await _orderService.PlaceOrderAsync(order);
            if (result)
                return Created("", order);
            return BadRequest();
        }
    }
}
