using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class OrderController : Controller
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    // 🔹 GET: Order List
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.ShippingDetail)
            .ToListAsync();

        return View(orders);
    }
    // GET
    public IActionResult Create()
    {
        ViewBag.Customers = _context.Customers.ToList();
        return View();
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // shipping auto add
        var shipping = new ShippingDetail
        {
            Address = "Default Address",
            Status = "Pending",
            OrderId = order.OrderId
        };

        _context.ShippingDetails.Add(shipping);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // 🔹 OPTIONAL: Details Page (safe)
    public async Task<IActionResult> Details(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.ShippingDetail)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
            return NotFound();

        return View(order);
    }

    // 🔹 TEST DATA ADD (very useful)
    public async Task<IActionResult> Seed()
    {
        // Customer add
        var customer = new Customer
        {
            Name = "Test User",
            Email = "test@gmail.com"
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        // Order add
        var order = new Order
        {
            CustomerId = customer.CustomerId,
            OrderDate = DateTime.Now
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Shipping add
        var shipping = new ShippingDetail
        {
            Address = "Delhi",
            Status = "Pending",
            OrderId = order.OrderId
        };

        _context.ShippingDetails.Add(shipping);
        await _context.SaveChangesAsync();

        return Content("Order Seeded Successfully");
    }
}