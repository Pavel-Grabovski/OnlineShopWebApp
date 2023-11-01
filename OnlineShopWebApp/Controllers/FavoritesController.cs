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

        public async Task<IActionResult> Index()
        {
            var products = await favoriteRepository.GetAllAsync(Constants.UserId);
            var productsViewModels = mapper.Map<ICollection<ProductViewModel>>(products);
            return View(productsViewModels);
        }
        public async Task<IActionResult> AddAsync(Guid productId)
        {
            var product = await productRepository.TryGetByIdAsync(productId);
            if(product != null)
            {
               await favoriteRepository.AddAsync(Constants.UserId, product);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveAsync(Guid productId)
        {
            var product = await productRepository.TryGetByIdAsync(productId);
            if (product != null)
            {
                await favoriteRepository.RemoveAsync(Constants.UserId, product);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ClearAsync()
        {
            await favoriteRepository.ClearAsync(Constants.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
