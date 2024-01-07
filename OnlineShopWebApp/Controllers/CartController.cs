using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.ViewsModels;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly ICartsServices cartsServices;
        private readonly IProductsServices productsServices;
        private readonly IMapper mapper;

        public CartController(ICartsServices cartsServices, IProductsServices productsServices, IMapper mapper)
        {
            this.cartsServices = cartsServices;
            this.productsServices = productsServices;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await cartsServices.TryGetByLoginAsync(User.Identity.Name);

            if (cart != null && cart.Items.Count() > 0)
            {
                var cartVM = mapper.Map<CartViewModel>(cart);

                return View(cartVM);
            }
            return View();
        }
        
        public async Task<IActionResult> AddAsync(Guid productId)
        {
            await cartsServices.AddAsync(User.Identity.Name, productId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseAmount(Guid productId)
        {
            await cartsServices.DecreaseAmountAsync(User.Identity.Name, productId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveAsync(Guid productId)
        {
            await cartsServices.RemoveAsync(User.Identity.Name, productId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ClearAsync()
        {
            await cartsServices.ClearAsync(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}
