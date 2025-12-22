using System.Diagnostics;
using FurniMPA101.App.Contexts;
using FurniMPA101.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurniMPA101.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products=_context.Products.Skip(0).Take(3).ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
