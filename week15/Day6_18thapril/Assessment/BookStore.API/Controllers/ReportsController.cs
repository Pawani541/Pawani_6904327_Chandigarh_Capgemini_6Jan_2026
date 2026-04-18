using Asp.Versioning;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/reports")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderReport(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (from > to)
                return BadRequest(new { message = "From date cannot be greater than To date." });

            var report = await _reportService.GetOrderReportAsync(from, to);
            return Ok(report);
        }

        [HttpGet("lowstock")]
        public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 5)
        {
            var books = await _reportService.GetLowStockBooksAsync(threshold);
            return Ok(books);
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var revenue = await _reportService.GetTotalRevenueAsync();
            return Ok(new { totalRevenue = revenue });
        }
    }
}
