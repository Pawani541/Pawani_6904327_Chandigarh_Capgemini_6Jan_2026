using E_Commerece_Application.Models;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerece_Application.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EcommerceContext _context;

        public CustomerController(EcommerceContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Customers.ToList();
            return View(data);
        }
    }
}