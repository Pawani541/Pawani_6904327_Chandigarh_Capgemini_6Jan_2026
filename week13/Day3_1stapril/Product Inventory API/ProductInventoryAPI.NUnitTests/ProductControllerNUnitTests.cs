using Moq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProductInventoryAPI.Controllers;
using ProductInventoryAPI.Models;
using ProductInventoryAPI.Services;

namespace ProductInventoryAPI.NUnitTests
{
    [TestFixture]
    public class ProductControllerNUnitTests
    {
        private Mock<IProductService> _mockService;
        private ProductController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Test]
        public async Task GetProduct_ExistingId_ReturnsOkWithProduct()
        {
            var product = new Product { Id = 1, Name = "Laptop", Price = 75000, Stock = 10 };
            _mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            var result = await _controller.GetProduct(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            var returnedProduct = (Product)okResult.Value!;
            Assert.That(returnedProduct.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetProduct_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((Product?)null);

            var result = await _controller.GetProduct(99);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
