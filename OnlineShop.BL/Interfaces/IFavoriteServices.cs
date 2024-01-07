using OnlineShop.BL.Domains;

namespace OnlineShop.Db.Interfaces;

public interface IFavoriteServices
{
	Task AddAsync(string login, Guid productId);
	Task<IEnumerable<Product>> GetAllAsync(string login);
	Task RemoveAsync(string login, Guid productId);
    Task ClearAsync(string login);
}
