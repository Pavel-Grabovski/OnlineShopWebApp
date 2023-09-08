using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

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
            var favorites = favoritesRepository.TryGetByUserId(Constants.UserId);
            return View(favorites);
        }
        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            //favoritesRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            //favoritesRepository.Remove(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {

            favoritesRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
