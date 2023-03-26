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
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

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


        //[HttpGet]
        //public IActionResult Upsert(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        //create product
        //        ProductVM vm = new ProductVM
        //        {
        //            Product = new Product(),
        //            CategoryList = _unitOfWork.Category.GetAll().Select(
        //                u => new SelectListItem
        //                {
        //                    Text = u.Name,
        //                    Value = u.Id.ToString()
        //                }),
        //            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
        //               u => new SelectListItem
        //               {
        //                   Text = u.Name,
        //                   Value = u.Id.ToString()

        //               })
        //        };


        //        //return NotFound();
        //        return View(vm);


            
        //}

        //    else
        //    {
        //        //update product
        //        return View();
        //    }
            
        //}

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM vm = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
        if(id==null||id==0)
            {
                //create
                return View(vm);
            }
            else
            {
                //update
                vm.Product = _unitOfWork.Prodcut.GetFirstOrDefault(c => c.Id == id);
                return View(vm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Prodcut.Add(productVM.Product);
                   // _unitOfWork.Save();
                }
                else
                {
                    //update
                    _unitOfWork.Prodcut.Update(productVM.Product);
                    //_unitOfWork.Save();
                }
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        //public IActionResult Delete(int id)
        //{
        //    if (id == null || id == 0) return NotFound();
        //    //var category = _context.Categories.Find(id);
        //    //var category = _context.GetFirstOrDefault(c => c.Id == id); //Replace with UOW
        //    var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);


        //    if (category == null) return NotFound();
        //    return View(category);
        //}
       
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var categoriesList = _unitOfWork.Prodcut.GetAll("Category");
			return Json(new { data = categoriesList });

		}
        [HttpDelete]

        public IActionResult Delete(int? id)
        {

            var prodcut = _unitOfWork.Prodcut.GetFirstOrDefault(c => c.Id == id);

            if (prodcut == null)
            {
                return Json(new { success = false, message = "Error Deleting Product" });
            }
            
                _unitOfWork.Prodcut.Remove(prodcut);
                _unitOfWork.Save();
            return Json(new { success = true, message = "Product Deleted Successfuly" });

            //return RedirectToAction("Index");

        }
        #endregion
    }
}
