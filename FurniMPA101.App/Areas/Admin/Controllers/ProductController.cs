using System.IO;
using FurniMPA101.App.Contexts;
using FurniMPA101.App.Models;
using FurniMPA101.App.ViewModels.Product;
using FurniMPA101.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FurniMPA101.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products=await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Sekil formatinda olmalidir");
            }
            if (model.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Maximum length kecmek olmaz");

            }

            string uniqueFileName = Guid.NewGuid().ToString() + model.Image.FileName;

            string imageUrl = Path.Combine(_environment.WebRootPath, "images", uniqueFileName);

            using FileStream fileStream = new FileStream(imageUrl, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);

            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                ImageName = model.ImageName,
                IsDeleted = false,
                ImageUrl= uniqueFileName
            };
                         
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

         

            string folder = Path.Combine(_environment.WebRootPath, "images");
            string imagePathForDelete = Path.Combine(folder, product.ImageUrl);
            System.IO.File.Delete(imagePathForDelete);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is not { })
            {
                return NotFound();
            }
            ProductUpdateVM model = new()
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                ImageName = product.ImageName,
                ImageUrl = product.ImageUrl
            };
            return View(model);
 
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existproduct = await _context.Products.FindAsync(model.Id);
            if (existproduct is not { })
            {
                return NotFound();
            }

           
           
            if (!model.Image?.ContentType.Contains("image") ?? false)
            {
                ModelState.AddModelError("Image", "Sekil tipi duzgun deyil");
                return View(model);
            }
            if (!model.Image?.CheckSize(2)??false)
            {
                ModelState.AddModelError("Image", "Maximum length size kecilib");
                return View(model);

            }


            if (model.Image is { })
            {
                string folderPath = Path.Combine(_environment.WebRootPath, "images");
                string uniqueImageName = Guid.NewGuid().ToString() + model.Image.FileName;

                string NewImagepath = Path.Combine(folderPath,uniqueImageName);
                string existImagePath = Path.Combine(folderPath, existproduct.ImageUrl);

                using FileStream stream = new FileStream(NewImagepath, FileMode.Create);
                await model.Image.CopyToAsync(stream);


                if (System.IO.File.Exists(existImagePath))
                    System.IO.File.Delete(existImagePath);


                existproduct.ImageUrl = uniqueImageName;

            }


            existproduct.Name = model.Name;
            existproduct.UpdatedDate = DateTime.Now;
            existproduct.Price = model.Price;
            existproduct.ImageName = model.ImageName;
            _context.Products.Update(existproduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> SoftDelete(int id)
        {
            var foundProduct = await _context.Products.FindAsync(id);
            if (foundProduct is not { })
            {
                return NotFound();
            }

            foundProduct.IsDeleted = !foundProduct.IsDeleted;
            _context.Products.Update(foundProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
