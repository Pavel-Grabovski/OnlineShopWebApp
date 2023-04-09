using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IFavoritesRepository
    {
        void Add(Product product, string userId);
        Favorites TryGetByUserId(string userId);
        void Remove(Product product, string userId);
        void Clear(string userId);
    }
}
