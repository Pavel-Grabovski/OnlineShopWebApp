using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces
{
	public interface IProductsServicies
	{
		Task<IEnumerable<Product>> GetAllAsync();
		Task<Product> TryGetByIdAsync(Guid id);
		Task DeleteAsync(Product product);
		Task AddAsync(Product product);
		Task UpdateAsync(Product product);
	}
}
