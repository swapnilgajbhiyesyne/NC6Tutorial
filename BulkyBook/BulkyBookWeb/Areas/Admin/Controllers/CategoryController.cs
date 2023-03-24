using BulkyBook.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //Replace ApplicatioDbContext with ICateoryRepository
        //private readonly ApplicationDbContext _context;
        //private ICategoryRepository _context;//REmoved as UOW implemetneted
        private IUnitOfWork _unitOfWork;
        //public CategoryController(ICategoryRepository context)
        //{
        //    _context = context;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //REplace with Category Repo methods
            //var categoriesList = _context.Categories.ToList();
            //IEnumerable<Category> categories = _context.Categories;
            //IEnumerable<Category> categories=_context.GetAll(); Removed as UOW
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();

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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order name cannot be same Nmae");
            }
            //Server Validation

            if (ModelState.IsValid)
            {

                //Replace with Category Repos Metod
                //_context.Categories.Add(obj);
                // _context.SaveChanges();
                //_context.Add(obj);
                //_context.Save();
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var Category = _context.Categories.Find(id);
            //var Category = _context.GetFirstOrDefault(c => c.Id == id);//Removed becoz UOW
            var Category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);


            if (Category == null) return NotFound();
            return View(Category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Update(obj);
                //_context.SaveChanges();
                //_context.Update(obj);
                //_context.Save();
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0) return NotFound();
            //var category = _context.Categories.Find(id);
            //var category = _context.GetFirstOrDefault(c => c.Id == id); //Replace with UOW
            var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);


            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            if (id == null || id == 0) return NotFound();
            // var category = _context.Categories.Find(id);
            //var category = _context.GetFirstOrDefault(c => c.Id == id);//replcae with UOW
            var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();
            //_context.Categories.Remove(category);
            //_context.SaveChanges();
            //_context.Remove(category);
            //_context.Save();
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
		
	}


}
