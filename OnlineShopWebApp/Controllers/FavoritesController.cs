using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IFavoriteRepository favoriteRepository;

        public FavoritesController(IProductsRepository productRepository, IFavoriteRepository favoritesRepository)
        {
            this.productRepository = productRepository;
            this.favoriteRepository = favoritesRepository;
        }

        public IActionResult Index()
        {
            var products = favoriteRepository.GetAll(Constants.UserId);
            var productsViewModels = products.ToProductsViewModels();
            return View(productsViewModels);
        }
        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            if(product != null)
            {
                favoriteRepository.Add(Constants.UserId, product);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            if (product != null)
            {
                favoriteRepository.Remove(Constants.UserId, product);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            favoriteRepository.Clear(Constants.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
