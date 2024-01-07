using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites;

public class FavoritesViewComponent : ViewComponent
{
    private readonly IFavoriteServices favoriteServices;

    public FavoritesViewComponent(IFavoriteServices favoriteServices)
    {
        this.favoriteServices = favoriteServices;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var favoritesProduct = await favoriteServices.GetAllAsync(User.Identity.Name);
        int productCount = favoritesProduct?.Count() ?? 0;

        return View("Favorites", productCount);
    }
}
