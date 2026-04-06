using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Services
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
    }
}
