using FurniMPA101.App.Models;
using Microsoft.EntityFrameworkCore;

namespace FurniMPA101.App.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
        
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
