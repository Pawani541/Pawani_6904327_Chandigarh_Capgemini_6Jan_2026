using ECommerceOrderManagement.API.DTOs;

namespace ECommerceOrderManagement.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}
