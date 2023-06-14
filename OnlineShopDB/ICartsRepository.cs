using OnlineShopDB.Models;

namespace OnlineShopDB
{
    public interface ICartsRepository
    {
        void Add(Product product, string userId);
        Cart TryGetByUserId(string userId);
        void Clear(string userId);
        List <CartItem> GetAll(Guid cartId);
        void Remove(Guid cartItemId, string userId);
        void DecreaseAmount(Guid cartItemId, string userId);
    }
}
