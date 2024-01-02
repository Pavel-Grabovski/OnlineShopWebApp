using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces;

public interface IProductsServices
{
	Task<IEnumerable<Product>> GetAllAsync();
	Task<Product> TryGetByIdAsync(Guid id);
	Task DeleteAsync(Product product);
	Task DeleteAsync(Guid id);
    Task AddAsync(Product product);
	Task UpdateAsync(Product product);
}
