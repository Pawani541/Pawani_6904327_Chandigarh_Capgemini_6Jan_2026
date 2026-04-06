using System.Text.Json;
using ECommerceOrderManagement.API.Data;
using ECommerceOrderManagement.API.DTOs;
using ECommerceOrderManagement.API.Models;
using ECommerceOrderManagement.API.Repositories.Interfaces;
using ECommerceOrderManagement.API.Services.Interfaces;

namespace ECommerceOrderManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICacheService _cache;
        private readonly AppDbContext _ctx;

        public ProductService(IProductRepository repo, ICacheService cache, AppDbContext ctx)
        {
            _repo = repo; _cache = cache; _ctx = ctx;
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var cached = await _cache.GetAsync("all_products");
            if (cached != null)
                return JsonSerializer.Deserialize<List<ProductResponseDto>>(cached)!;
            var result = (await _repo.GetAllAsync()).Select(Map).ToList();
            await _cache.SetAsync("all_products", JsonSerializer.Serialize(result), TimeSpan.FromMinutes(5));
            return result;
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : Map(p);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            var p = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ProductCategories = dto.CategoryIds
                    .Select(id => new ProductCategory { CategoryId = id }).ToList()
            };
            await _repo.AddAsync(p);
            await _cache.RemoveAsync("all_products");
            return Map(p);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;
            await _repo.DeleteAsync(p);
            await _cache.RemoveAsync("all_products");
            return true;
        }

        private static ProductResponseDto Map(Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            Categories = p.ProductCategories.Select(pc => pc.Category?.Name ?? "").ToList()
        };
    }
}
