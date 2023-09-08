using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface ICartsRepository
    {
        void Add(ProductViewModel product, string userId);
        Cart TryGetByUserId(string userId);
        void DecreaseAmount(ProductViewModel product, string userId);
        void Remove(ProductViewModel product, string userId);
        void Clear(string userId);
    }
}
