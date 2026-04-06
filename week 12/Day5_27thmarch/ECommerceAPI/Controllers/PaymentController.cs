using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PaymentController));

    [HttpPost]
    public IActionResult Pay()
    {
        log.Info("Payment request started");

        var sw = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            Thread.Sleep(6000); // delay

            sw.Stop();

            if (sw.ElapsedMilliseconds > 5000)
            {
                log.Warn("Payment delay > 5 sec");
            }

            throw new Exception("Timeout");
        }
        catch (Exception ex)
        {
            log.Error("Payment failed", ex);
            return StatusCode(500);
        }
    }
}