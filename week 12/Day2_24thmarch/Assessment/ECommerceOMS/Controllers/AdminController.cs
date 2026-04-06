using ECommerceOMS.Data;
using ECommerceOMS.Models;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Dashboard()
        {
            var topProducts = await _db.OrderItems
                .GroupBy(oi => oi.Product.Name)
                .Select(g => new TopProduct
                {
                    ProductName = g.Key,
                    TotalSold = g.Sum(oi => oi.Quantity),
                    Revenue = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync();

            var pending = await _db.Orders
                .Include(o => o.Customer)
                .Where(o => o.Status == OrderStatus.Pending)
                .ToListAsync();

            var summaries = await _db.Customers
                .Select(c => new CustomerSummary
                {
                    Customer = c,
                    OrderCount = c.Orders.Count,
                    TotalSpent = c.Orders.Sum(o => o.TotalAmount)
                })
                .ToListAsync();

            return View(new DashboardViewModel
            {
                TopProducts = topProducts,
                PendingOrders = pending,
                CustomerSummaries = summaries
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShipping(int shippingId, string status)
        {
            var shipping = await _db.ShippingDetails.FindAsync(shippingId);
            if (shipping != null)
            {
                shipping.Status = status;
                if (status == "Shipped") shipping.ShippedDate = DateTime.Now;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}