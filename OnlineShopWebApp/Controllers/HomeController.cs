using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopWebApp.Models;
using System.Diagnostics;

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
            var productsDB = productRepository.GetAll();

            var productsViewModels = new List<ProductViewModel>();
            foreach(var productDB in productsDB)
            {
                var productViewModel = new ProductViewModel
                {
                    Id = productDB.Id,
                    Name = productDB.Name,
                    Cost = productDB.Cost,
                    Description = productDB.Description,
                    ImagePath = productDB.ImagePath
                };
                productsViewModels.Add(productViewModel);
            }

            return View(productsViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}