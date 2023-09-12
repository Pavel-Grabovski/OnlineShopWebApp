using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productRepository;

        public ProductController(IProductsRepository productsRepository)
        {
            this.productRepository = productsRepository;
        }

        public IActionResult Index(Guid id)
        {
            var product = productRepository.TryGetById(id);
            if (product != null)
            {
                var productVM = product.ToProductViewModel();
                return View(productVM);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
