using FurniMPA101.App.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniMPA101.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
       
        public CustomerController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            var customers=await _context.Customers.ToListAsync();
            return View(customers);
        }
    }
}
