using BookStore.Application.DTOs;
using BookStore.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderApiService _orderService;

        public OrdersController(IOrderApiService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            IEnumerable<OrderResponseDto> orders;
            if (userRole == "Admin")
                orders = await _orderService.GetAllOrdersAsync();
            else
            {
                var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                orders = await _orderService.GetOrdersByUserAsync(userId);
            }

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        public IActionResult Checkout()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var order = await _orderService.PlaceOrderAsync(dto);
            TempData["Success"] = $"Order #{order.OrderId} placed successfully!";
            return RedirectToAction("Details", new { id = order.OrderId });
        }
    }
}
