using Moq;
using Microsoft.AspNetCore.Mvc;
using ProductInventoryAPI.Controllers;
using ProductInventoryAPI.Models;
using ProductInventoryAPI.Services;

namespace ProductInventoryAPI.xUnitTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task GetProduct_ExistingId_ReturnsOkWithProduct()
        {
            var product = new Product { Id = 1, Name = "Laptop", Price = 75000, Stock = 10 };
            _mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            var result = await _controller.GetProduct(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, returnedProduct.Id);
        }

        [Fact]
        public async Task GetProduct_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((Product?)null);

            var result = await _controller.GetProduct(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
