using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ECommerceOrderManagement.MVC.Models;

namespace ECommerceOrderManagement.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public HomeController(IHttpClientFactory factory) { _factory = factory; }

        public async Task<IActionResult> Index()
        {
            var client = _factory.CreateClient("API");
            var response = await client.GetAsync("/api/products");
            var body = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<ProductViewModel>>(body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            return View(products);
        }
    }
}
