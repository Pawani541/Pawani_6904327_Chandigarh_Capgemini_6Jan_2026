using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ECommerceOrderManagement.MVC.Models;

namespace ECommerceOrderManagement.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public OrderController(IHttpClientFactory factory) { _factory = factory; }

        private HttpClient GetAuthClient()
        {
            var client = _factory.CreateClient("API");
            var token = HttpContext.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task<IActionResult> AllOrders()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");
            var client = GetAuthClient();
            var response = await client.GetAsync("/api/orders");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(body))
                return View(new List<OrderViewModel>());
            var orders = JsonSerializer.Deserialize<List<OrderViewModel>>(body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            return View(orders);
        }

        public async Task<IActionResult> MyOrders()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");
            var client = GetAuthClient();
            var userId = HttpContext.Session.GetString("UserId") ?? "1";
            var response = await client.GetAsync($"/api/orders/user/{userId}");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(body))
                return View(new List<OrderViewModel>());
            var orders = JsonSerializer.Deserialize<List<OrderViewModel>>(body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Place(int productId)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int ProductId, int Quantity)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var client = GetAuthClient();
            var userId = int.Parse(HttpContext.Session.GetString("UserId") ?? "1");

            var json = JsonSerializer.Serialize(new
            {
                UserId = userId,
                Items = new[] { new { ProductId, Quantity } }
            });

            var response = await client.PostAsync("/api/orders",
                new StringContent(json, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = $"Order failed ({response.StatusCode}): {responseBody}";
                ViewBag.ProductId = ProductId;
                return View("Place");
            }
            return RedirectToAction("MyOrders");
        }
    }
}




