using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;

        public CartViewComponent(ICartsRepository cartsRepository)
        {
            this.cartsRepository = cartsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            int productCount = cart?.CartItems.Count ?? 0 ;

            return View("Cart", productCount);
        }
    }
}
