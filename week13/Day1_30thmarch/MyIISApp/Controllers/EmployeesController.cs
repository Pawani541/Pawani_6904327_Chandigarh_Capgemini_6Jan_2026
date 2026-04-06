using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyIISApp.Data;
using MyIISApp.Models;

namespace MyIISApp.Controllers
{
    [Authorize] // 🔐 Secure API (IMPORTANT)
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/employees
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
                return NotFound("Employee not found");

            return Ok(emp);
        }

        // 🔹 POST
        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            emp.Id = 0; 

            _context.Employees.Add(emp);
            _context.SaveChanges();

            return Ok(emp);
        }

        // 🔹 PUT (Update)
        [HttpPut("{id}")]
        public IActionResult Put(int id, Employee emp)
        {
            var existing = _context.Employees.Find(id);
            if (existing == null)
                return NotFound("Employee not found");

            existing.Name = emp.Name;
            existing.Email = emp.Email;

            _context.SaveChanges();

            return Ok(existing);
        }

        // 🔹 DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
                return NotFound("Employee not found");

            _context.Employees.Remove(emp);
            _context.SaveChanges();

            return Ok("Deleted successfully");
        }
    }
}