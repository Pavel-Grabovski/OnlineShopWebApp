using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopDB.Models;
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
            if(cart != null)
            {
                var cartItems = cartsRepository.GetAll(cart.Id);

                var cartItemsViewModels = new List<CartItemViewModel>();

                foreach (var cartItem in cartItems) 
                {
                    var product = productRepository.TryGetById(cartItem.ProductId);
                    var cartItemViewModel = new CartItemViewModel()
                    {
                        Id = cartItem.Id,
                        Product = new ProductViewModel()
                        {
                            Id = cartItem.ProductId,
                            Name = product.Name,
                            Cost = product.Cost,
                            Description = product.Description,
                            ImagePath = product.ImagePath,
                        },
                        Amount = cartItem.Amount,
                    };
                    cartItemsViewModels.Add(cartItemViewModel);
                }

                var cartViewModel = new CartViewModel
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    Items = cartItemsViewModels
                };

                return View(cartViewModel);
            }
            return View();
        }
        public IActionResult Add(Guid productId)
        {
            var product = productRepository.TryGetById(productId);
            cartsRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult DecreaseAmount(Guid cartItemId)
        {
            cartsRepository.DecreaseAmount(cartItemId, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid cartItemId)
        {
            cartsRepository.Remove(cartItemId, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
