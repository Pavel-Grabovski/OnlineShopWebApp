using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productRepository;
        public HomeController(IProductsRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var productsDb = productRepository.GetAll();
            var productsViewModels = productsDb.ToProductsViewModels();
            return View(productsViewModels);
        }
    }
}