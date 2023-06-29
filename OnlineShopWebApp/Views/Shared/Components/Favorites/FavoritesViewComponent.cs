using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
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
            var favorites = favoritesRepository.GetAll(Constants.UserId);
            int productCount = favorites?.Count ?? 0;

            return View("Favorites", productCount);
        }
    }
}
