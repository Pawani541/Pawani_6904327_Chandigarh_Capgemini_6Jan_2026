using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderAPI.Api.Controllers;
using OrderAPI.Api.Models;
using OrderAPI.Api.Services;
using Xunit;

namespace OrderAPI.Tests.xUnit
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockService = new Mock<IOrderService>();
            _controller = new OrderController(_mockService.Object);
        }

        [Fact]
        public async Task PlaceOrder_ValidOrder_Returns201Created()
        {
            var order = new Order { Id = 1, CustomerName = "Pawani", Amount = 500 };
            _mockService.Setup(s => s.PlaceOrderAsync(order)).ReturnsAsync(true);

            var result = await _controller.PlaceOrder(order);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task PlaceOrder_InvalidOrder_Returns400BadRequest()
        {
            var order = new Order { Id = 2, CustomerName = "", Amount = 0 };
            _mockService.Setup(s => s.PlaceOrderAsync(order)).ReturnsAsync(false);

            var result = await _controller.PlaceOrder(order);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
