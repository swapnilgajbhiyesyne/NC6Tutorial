using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categoriesList = _context.Categories.ToList();
            IEnumerable<Category> categories = _context.Categories;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            //Custom Validation
            if(obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order name cannot be same Nmae");
            }
            //Server Validation

            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var Category = _context.Categories.Find(id);
            if (Category == null) return NotFound();
            return View(Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        { 
        if(ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0) return NotFound();
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            if (id == null || id == 0) return NotFound();
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
