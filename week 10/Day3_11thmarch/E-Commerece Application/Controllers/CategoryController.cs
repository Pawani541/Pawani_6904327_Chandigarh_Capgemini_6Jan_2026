using Microsoft.AspNetCore.Mvc;

namespace E_Commerece_Application.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
