using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoriteRepository
    {
        Task AddAsync(string userId, Product product);
        Task<ICollection<Product>> GetAllAsync(string userId);
        Task RemoveAsync(string userId, Product product);
        Task ClearAsync(string userId);
    }
}
