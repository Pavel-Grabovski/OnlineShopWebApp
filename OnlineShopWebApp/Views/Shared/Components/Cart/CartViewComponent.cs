using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;

        public CartViewComponent(ICartsRepository cartsRepository)
        {
            this.cartsRepository = cartsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await cartsRepository.TryGetByUserIdAsync(Constants.UserId);
            int productCount = 0;
            if (cart != null)
            {
                productCount = cart.Items.Sum(x => x.Amount);
            }
            return View("Cart", productCount);
        }
    }
}
