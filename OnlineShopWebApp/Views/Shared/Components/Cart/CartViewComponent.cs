using Microsoft.AspNetCore.Mvc;
using OnlineShop.BL;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsServices cartsServices;

        public CartViewComponent(ICartsServices cartsServices)
        {
            this.cartsServices = cartsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await cartsServices.TryGetByLoginAsync(User.Identity.Name);

            int count = 0;
            if (cart != null)
                count = cart.Items.Sum(x => x.Amount);

            return View("Cart", count);
        }
    }
}
