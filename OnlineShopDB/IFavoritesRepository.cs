
using OnlineShopDB.Models;

namespace OnlineShopDB
{
    public interface IFavoritesRepository
    {
        void Add(string userId, Product product);
        ICollection<Product> GetAll(string userId);
        void Remove(string userId, Product product);
        void Clear(string userId);
    }
}
