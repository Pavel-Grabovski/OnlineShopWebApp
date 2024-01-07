using Microsoft.AspNetCore.Mvc;
using OnlineShop.BL.Interfaces;

namespace OnlineShopWebApp.Views.Shared.Components.Cart;

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
        int сount = cart?.Items?.Count() ?? 0;
        return View("Cart", сount);
    }
}
