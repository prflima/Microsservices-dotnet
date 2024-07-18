using GeekShopping.WEB.Models;
using GeekShopping.WEB.Services.IServices;
using GeekShopping.WEB.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
            var token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.GetProducts(token);
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid) 
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.Create(model, token);
                if (response != null) 
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.GetById(id, token);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
		[Authorize]
		public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.Update(model, token);
                if (response != null)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
		[Authorize(Roles = Role.Admin)]
		public async Task<IActionResult> ProductDelete(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.Delete(id, token);
            if(response)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
