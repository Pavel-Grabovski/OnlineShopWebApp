using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopWebApp.Models;

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
                var productVM = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                };
                return View(productVM);
            }
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
