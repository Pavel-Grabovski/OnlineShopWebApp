using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly IFavoriteRepository favoritesRepository;

        public FavoritesViewComponent(IFavoriteRepository favoritesRepository)
        {
            this.favoritesRepository = favoritesRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var favorites = await favoritesRepository.GetAllAsync(Constants.UserId);
            int productCount = favorites?.Count ?? 0;

            return View("Favorites", productCount);
        }
    }
}
