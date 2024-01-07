using OnlineShop.Db.Entities;

namespace OnlineShop.Db.Interfaces;

public interface IFavoriteRepository
{
    Task AddAsync(string userId, Guid productId);
    Task<IEnumerable<ProductEntity>> GetAllAsync(string userId);
    Task RemoveAsync(string userId, Guid productId);
    Task ClearAsync(string userId);
}
