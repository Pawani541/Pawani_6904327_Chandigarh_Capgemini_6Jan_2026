using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Books");
        }

        public IActionResult Privacy() => View();

        public IActionResult Error() => View();
    }
}
