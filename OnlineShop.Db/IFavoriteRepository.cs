using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IFavoriteRepository
    {
        void Add(string userId, Product product);
        ICollection<Product> GetAll(string userId);
        void Remove(string userId, Product product);
        void Clear(string userId);
    }
}
