using Microsoft.AspNetCore.Mvc;

namespace SmartHealthcare.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }

    public class DoctorController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public DoctorController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index() => View();
    }

    public class PatientController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public PatientController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index() => View();
    }

    public class AppointmentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public AppointmentController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index() => View();
        public IActionResult Book() => View();
    }

    public class AccountController : Controller
    {
        public IActionResult Login() => View();
        public IActionResult Register() => View();
        public IActionResult Logout() => RedirectToAction("Index", "Home");
    }
}
