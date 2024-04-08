using GeekShopping.WEB.Models;
using GeekShopping.WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.WEB.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid) 
            {
                var response = await _productService.Create(model);
                if (response != null) 
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.Update(model);
                if (response != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(long id)
        {
            var response = await _productService.Delete(id);
            if(response)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
