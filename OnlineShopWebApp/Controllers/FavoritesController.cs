using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IMapper mapper;
        public FavoritesController(IProductsRepository productRepository, IFavoriteRepository favoritesRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.favoriteRepository = favoritesRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = favoriteRepository.GetAll(Constants.UserId);
            var productsViewModels = mapper.Map<ICollection<ProductViewModel>>(products);
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
