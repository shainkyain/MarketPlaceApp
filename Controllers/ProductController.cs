using MarketPlaceApp.Models;
using MarketPlaceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService = new ProductService();
        public IActionResult AllProductList()
        {
            List<Product> allProducts = productService.GetAllProduct();
            return View(allProducts);
        }

        public IActionResult ProductPage(int id)
        {
            Product Product = productService.GetAllProduct().FirstOrDefault(u => u.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);

        }
        public IActionResult EditProduct(int id)
        {
            var user = HttpContext.Session.GetString("LoggedUserRole");

            if (user != "Admin") { return RedirectToAction("SomethingWentWrong", "Home"); }

            Product product = productService.GetAllProduct().FirstOrDefault(u => u.Id == id);
            return View(product);
        }

        public IActionResult UpdateProduct(Product product)
        {
            if (product == null)
            {
                return RedirectToAction("SomethingWentWrong", "Home");
            }


            productService.UpdateProduct(product);
            return RedirectToAction("AllProductList", "Product");
        }
        public IActionResult CreateProduct()
        {
            var role = HttpContext.Session.GetString("LoggedUserRole");
            if(role != "Admin")
            {
                return RedirectToAction("SomethingWentWrong", "Home");
            }
            return View();
        }

        public IActionResult CreateProductMethod(Product product)
        {
            if (product == null)
            {
                return RedirectToAction("SomethingWentWrong", "Home");
            }
            productService.AddProduct(product);
            return RedirectToAction("AllProductList", "Product");
        }
    }
}
