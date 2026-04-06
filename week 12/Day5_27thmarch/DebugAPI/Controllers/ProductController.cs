using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ProductController));

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var start = DateTime.Now;
        log.Info("API Started");

        try
        {
            Thread.Sleep(4000); // simulate slow API

            var duration = DateTime.Now - start;

            if (duration.TotalSeconds > 3)
                log.Warn($"Slow API detected: {duration.TotalSeconds}");
                 log.Info("API Completed"); 

            return Ok(new { Id = id, Name = "Laptop" });
        }
        catch (Exception ex)
        {
            log.Error("API failed", ex);
            return StatusCode(500);
        }
    }
}