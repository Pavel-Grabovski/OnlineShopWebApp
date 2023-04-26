using Microsoft.AspNetCore.Mvc;

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
            productRepository.DeleteProduct(productId);
            return RedirectToAction("Products");
        }
        public IActionResult Roles()
        {
            return View();
        }
    }
}
