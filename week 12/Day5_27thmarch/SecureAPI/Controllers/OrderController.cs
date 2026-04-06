using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OrdersController));

    [Authorize]
    [HttpGet]
    public IActionResult GetOrders()
    {
        log.Info("GET /api/orders called");
        return Ok(new[] { "Order1", "Order2" });
    }
}