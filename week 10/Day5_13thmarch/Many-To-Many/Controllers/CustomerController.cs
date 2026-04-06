using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public IActionResult Index()
{
    var data = _context.Customers
        .Include(c => c.Orders)
        .ThenInclude(o => o.Product)
        .ToList();

    return View(data);
}