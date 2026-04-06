using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeProjectManagement.Data;
using EmployeeProjectManagement.Models;
using System;
using System.Linq;

namespace EmployeeProjectManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Employee List
        public IActionResult Index()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .ToList();

            return View(employees);
        }

        // Create Employee Page
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Projects = _context.Projects.ToList();

            return View();
        }

        // Save Employee
        [HttpPost]
        public IActionResult Create(Employee employee, int[] selectedProjects)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            if (selectedProjects != null)
            {
                foreach (var projectId in selectedProjects)
                {
                    EmployeeProject ep = new EmployeeProject()
                    {
                        EmployeeId = employee.EmployeeId,
                        ProjectId = projectId,
                        AssignedDate = DateTime.Now
                    };

                    _context.EmployeeProjects.Add(ep);
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}