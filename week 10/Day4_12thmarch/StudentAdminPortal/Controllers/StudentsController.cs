using Microsoft.AspNetCore.Mvc;

namespace StudentAdminPortal.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}