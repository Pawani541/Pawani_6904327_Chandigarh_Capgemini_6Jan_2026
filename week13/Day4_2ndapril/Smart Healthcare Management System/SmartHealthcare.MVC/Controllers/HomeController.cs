using Microsoft.AspNetCore.Mvc;

namespace SmartHealthcare.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("JwtToken") == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role     = HttpContext.Session.GetString("Role");
            return View();
        }
    }
}
