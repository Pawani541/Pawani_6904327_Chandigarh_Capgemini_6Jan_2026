using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OrderAPI.Api.Controllers;
using OrderAPI.Api.Models;
using OrderAPI.Api.Services;

namespace OrderAPI.Tests.NUnit
{
    [TestFixture]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockService;
        private OrderController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IOrderService>();
            _controller = new OrderController(_mockService.Object);
        }

        [Test]
        public async Task PlaceOrder_ValidOrder_Returns201Created()
        {
            var order = new Order { Id = 1, CustomerName = "Pawani", Amount = 500 };
            _mockService.Setup(s => s.PlaceOrderAsync(order)).ReturnsAsync(true);

            var result = await _controller.PlaceOrder(order);

            Assert.That(result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        public async Task PlaceOrder_InvalidOrder_Returns400BadRequest()
        {
            var order = new Order { Id = 2, CustomerName = "", Amount = 0 };
            _mockService.Setup(s => s.PlaceOrderAsync(order)).ReturnsAsync(false);

            var result = await _controller.PlaceOrder(order);

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}
