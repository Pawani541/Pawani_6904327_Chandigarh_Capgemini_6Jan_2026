using Microsoft.AspNetCore.Mvc;
using Middleware_for_Request_Tracking.Services;

namespace Middleware_for_Request_Tracking.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IRequestLogService _logService;

        public StudentsController(IRequestLogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            var logs = _logService.GetLogs();
            return View(logs);
        }

        // Extra request trigger karne ke liye
        public IActionResult Test()
        {
            return RedirectToAction("Index");
        }
    }
}