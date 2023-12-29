using OnlineShop.Entities;

namespace OnlineShop.Db;

public interface IFavoriteRepository
{
    Task AddAsync(string userId, Product product);
    Task<ICollection<Product>> GetAllAsync(string userId);
    Task RemoveAsync(string userId, Product product);
    Task ClearAsync(string userId);
}
