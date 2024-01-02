using OnlineShop.BL.Domains;

namespace OnlineShop.Db.Interfaces;

public interface IFavoriteServices
{
	Task AddAsync(string userId, Product product);
	Task<IEnumerable<Product>> GetAllAsync(string userId);
	Task RemoveAsync(string userId, Product product);
	Task ClearAsync(string userId);
}
