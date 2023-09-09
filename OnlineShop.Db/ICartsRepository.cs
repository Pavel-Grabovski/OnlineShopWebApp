using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface ICartsRepository
    {
        void Add(string userId, Product product);
        Cart TryGetByUserId(string userId);
        void DecreaseAmount(string userId, Product product);
        void Remove(string userId, Product product);
        void Clear(string userId);
    }
}
