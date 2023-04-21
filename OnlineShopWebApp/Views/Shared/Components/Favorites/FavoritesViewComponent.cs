using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesViewComponent : ViewComponent
    {
        private readonly IFavoritesRepository favoritesRepository;

        public FavoritesViewComponent(IFavoritesRepository favoritesRepository)
        {
            this.favoritesRepository = favoritesRepository;
        }
        public IViewComponentResult Invoke()
        {
            var favorites = favoritesRepository.TryGetByUserId(Constants.UserId);
            int productCount = favorites?.Amount ?? 0;

            return View("Favorites", productCount);
        }
    }
}
