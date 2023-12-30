using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IMapper mapper;
        public CartController(IProductsRepository productRepository, ICartsRepository cartsRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.cartsRepository = cartsRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await cartsRepository.TryGetByUserIdAsync(Constants.UserId);
            if (cart != null && cart.Items.Count > 0)
            {
                var cartVM = mapper.Map<CartViewModel>(cart);

                return View(cartVM);
            }
            return View();
        }
        
        public async Task<IActionResult> AddAsync(Guid productId)
        {
            var product = await productRepository.TryGetByIdAsync(productId);
            await cartsRepository.AddAsync(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseAmount(Guid productId)
        {
            var product = await productRepository.TryGetByIdAsync(productId);
            if(product != null)
            {
                await cartsRepository.DecreaseAmountAsync(Constants.UserId, product);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveAsync(Guid productId)
        {
            var product = await productRepository.TryGetByIdAsync(productId);
            await cartsRepository.RemoveAsync(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ClearAsync()
        {
            await cartsRepository.ClearAsync(Constants.UserId);
            return RedirectToAction(nameof(Index));
        }
    }
}
