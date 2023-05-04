using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository productRepository;

        public AdminController(IProductsRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Products()
        {
            var products = productRepository.GetAll();
            return View(products);
        }
        public IActionResult DeleteProduct(int productId)
        {
            productRepository.Delete(productId);
            return RedirectToAction("Products");
        }

        public IActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(Product product)
        {
            productRepository.Add(product);
            return RedirectToAction("Products");
        }

        public IActionResult Roles()
        {
            return View();
        }
    }
}
