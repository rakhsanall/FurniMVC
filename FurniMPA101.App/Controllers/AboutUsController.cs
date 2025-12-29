using FurniMPA101.App.Contexts;
using FurniMPA101.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurniMPA101.App.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public AboutUsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _context.Employees.OrderBy(p=>p.Id).Skip(0).Take(4).ToList();
            return View(employees);
        }
    }
}
