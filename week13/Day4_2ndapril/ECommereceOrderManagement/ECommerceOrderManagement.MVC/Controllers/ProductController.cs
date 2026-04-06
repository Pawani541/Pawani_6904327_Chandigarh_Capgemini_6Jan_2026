using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ECommerceOrderManagement.MVC.Models;

namespace ECommerceOrderManagement.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public ProductController(IHttpClientFactory factory) { _factory = factory; }

        private HttpClient GetAuthClient()
        {
            var client = _factory.CreateClient("API");
            var token = HttpContext.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public IActionResult Add() => View(new CreateProductViewModel());

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductViewModel model)
        {
            var client = GetAuthClient();
            var json = JsonSerializer.Serialize(new
            {
                model.Name,
                model.Description,
                model.Price,
                model.StockQuantity,
                CategoryIds = new List<int>()
            });
            var response = await client.PostAsync("/api/products",
                new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to add product. Admin access required.";
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = GetAuthClient();
            await client.DeleteAsync($"/api/products/{id}");
            return RedirectToAction("Index", "Home");
        }
    }
}
