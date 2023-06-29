using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IFavoritesRepository favoritesRepository;

        public FavoritesController(IProductsRepository productRepository, IFavoritesRepository favoritesRepository)
        {
            this.productRepository = productRepository;
            this.favoritesRepository = favoritesRepository;
        }

        public IActionResult Index()
        {
            var favorites = favoritesRepository.GetAll(Constants.UserId);
            var favoritesVM = favorites.ToProductViewModels();
            return View(favoritesVM);
        }
        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            favoritesRepository.Add(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            favoritesRepository.Remove(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            favoritesRepository.Clear(Constants.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
