using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 75000, Stock = 10 },
            new Product { Id = 2, Name = "Mouse", Price = 500, Stock = 50 }
        };

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            await Task.CompletedTask;
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}
