using FurniMPA101.App.Contexts;
using FurniMPA101.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniMPA101.App.Areas.Admin.Controllers
{
  
        [Area("Admin")]
        public class BlogController : Controller
        {
            AppDbContext _context;
            public BlogController(AppDbContext context)
            {
                _context = context;
            }
            public async Task<IActionResult> Index()
            {
                List<Blog> blogs = await _context.Blogs.Include(x=>x.Employee).ToListAsync();
                return View(blogs);
            }
        public async Task<IActionResult> Create()
        {
            await SendEmployeesToViewBag();
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            await SendEmployeesToViewBag();
            await _context.Blogs.AddAsync(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        private async Task SendEmployeesToViewBag()
        {
            var employees = await _context.Employees.ToListAsync();
            ViewBag.Employees = employees;
        }




        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            await SendEmployeesToViewBag();
            var foundBlog = _context.Blogs.Find(id);
            if (foundBlog is not { })
            {

                return NotFound();
            }
            return View(foundBlog); 
        }
        [HttpPost]
        public async Task<IActionResult> Update(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                await SendEmployeesToViewBag();
                return View();
            }
            var foundBlog = await _context.Blogs.FindAsync(blog.Id);
            foundBlog.Title=blog.Title;
            foundBlog.Content = blog.Content;
            foundBlog.ImageName=blog.ImageName;
            foundBlog.ImageUrl=blog.ImageUrl;
            foundBlog.EmployeeId=blog.EmployeeId;

            _context.Blogs.Update(foundBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
        
}
