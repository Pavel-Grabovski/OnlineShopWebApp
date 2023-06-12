using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IFavoritesRepository
    {
        void Add(ProductViewModel product, string userId);
        Favorites TryGetByUserId(string userId);
        void Remove(ProductViewModel product, string userId);
        void Clear(string userId);
    }
}
