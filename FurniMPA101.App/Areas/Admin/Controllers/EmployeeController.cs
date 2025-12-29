using FurniMPA101.App.Contexts;
using FurniMPA101.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniMPA101.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees =await _context.Employees.ToListAsync();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var foundEmployee = _context.Employees.Find(id);
            if (foundEmployee is not { })
            {
                return NotFound();
            }
            _context.Employees.Remove(foundEmployee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var foundEmployee = _context.Employees.Find(id);
            if (foundEmployee is not { })
            {
                return NotFound();
            }
            return View(foundEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {         
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

    }
}
