using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
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
            if(cart != null && cart.Items.Count > 0)
            {
                var cartVM = cart.ToCartViewModel();
                return View(cartVM);
            }
            return View();
        }
        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            cartsRepository.Add(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DecreaseAmount(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            if(product != null)
            {
                cartsRepository.DecreaseAmount(Constants.UserId, product);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            cartsRepository.Remove(Constants.UserId, product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
