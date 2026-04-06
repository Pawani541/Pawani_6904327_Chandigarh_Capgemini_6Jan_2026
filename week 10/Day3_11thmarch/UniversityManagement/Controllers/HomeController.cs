using Microsoft.AspNetCore.Mvc;

namespace UniversityManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}