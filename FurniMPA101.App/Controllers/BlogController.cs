using FurniMPA101.App.Contexts;
using FurniMPA101.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniMPA101.App.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<Blog> blogs = _context.Blogs.OrderBy(p => p.Id).Include(x=>x.Employee).Skip(0).Take(9).ToList();
            return View(blogs);
        }
    }
}
