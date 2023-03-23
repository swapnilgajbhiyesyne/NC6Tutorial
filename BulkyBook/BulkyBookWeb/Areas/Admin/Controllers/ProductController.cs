using BulkyBook.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //Replace ApplicatioDbContext with ICateoryRepository
        //private readonly ApplicationDbContext _context;
        //private ICategoryRepository _context;//REmoved as UOW implemetneted
        private IUnitOfWork _unitOfWork;
        //public CategoryController(ICategoryRepository context)
        //{
        //    _context = context;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            

            return View();
        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //create product
                ProductVM vm = new ProductVM
                {
                    Product = new Product(),
                    CategoryList = _unitOfWork.Category.GetAll().Select(
                        u => new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        }),
                    CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                       u => new SelectListItem
                       {
                           Text = u.Name,
                           Value = u.Id.ToString()

                       })
                };


                //return NotFound();
                return View(vm);


            
        }

            else
            {
                //update product
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Prodcut.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
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
