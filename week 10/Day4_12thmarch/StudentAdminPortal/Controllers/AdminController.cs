using Microsoft.AspNetCore.Mvc;

namespace StudentAdminPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}