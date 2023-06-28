using OnlineShopDB.Models;

namespace OnlineShopDB
{
    public interface ICartsRepository
    {
        void Add(Product product, string userId);
        void Clear(string userId);
        void DecreaseAmount(Guid productId, string userId);
        ICollection<CartItem> GetAll(string userId);
        void Remove(string userId);
        Cart TryGetByUserId(string userId);
    }
}