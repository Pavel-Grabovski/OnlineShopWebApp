using OnlineShopDB.Models;

namespace OnlineShopDB
{
    public interface ICartsRepository
    {
        void Add(Product product, string userId);
        void Clear(string userId);
        void DecreaseAmount(string userId, Guid productId);
        ICollection<CartItem> GetAll(string userId);
        void Remove(string userId, Guid productId);
        Cart TryGetByUserId(string userId);
    }
}