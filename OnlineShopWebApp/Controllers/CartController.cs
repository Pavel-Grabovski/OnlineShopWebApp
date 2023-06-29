using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopDB.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly ICartsRepository cartsRepository;

        public CartController(IProductsRepository productRepository, ICartsRepository cartsRepository)
        {
            this.productRepository = productRepository;
            this.cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            if (cart != null && cart.CartItems.Count > 0)
            {
                var cartViewModel = cart.ToCartViewModel();
                return View(cartViewModel);
            }

            return View();
        }
        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            cartsRepository.Add(product, Constants.UserId);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DecreaseAmount(Guid productId)
        {
            cartsRepository.DecreaseAmount(Constants.UserId, productId);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(Guid productId)
        {
            cartsRepository.Remove(Constants.UserId, productId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
