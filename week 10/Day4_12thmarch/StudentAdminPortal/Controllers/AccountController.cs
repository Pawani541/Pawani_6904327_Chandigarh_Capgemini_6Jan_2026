using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace StudentAdminPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username)
        {
            HttpContext.Session.SetString("User", username);
            return RedirectToAction("Index", "Admin");
        }
    }
}