using CrudWithRepository.Core;
using CrudWithRepository.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            var products =await _productRepo.GetaAll();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new Product());
            }
            else
            {
                Product product = await _productRepo.GetById(id);
                if(product != null)
                {
                    return View(product);
                }
                TempData["errormessage"] = $"Product details not found with id:{id}";
                return RedirectToAction("Index");
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(model.Id == 0)
                    {
                        await _productRepo.Add(model);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        await _productRepo.Update(model);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["errormessage"] = "Invalid model";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
                
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = await _productRepo.GetById(id);
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {

                TempData["errormessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["errormessage"] = $"Product details not found with id: {id}";
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                 await _productRepo.Delete(id);
                TempData["errormessage"] = $"Product Deleted successfully!";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["errormessage"] = ex.Message;
                return View();
            }
            
        }
    }
}
