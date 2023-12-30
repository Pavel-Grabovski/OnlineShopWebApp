using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces
{
	public interface IProductsRepository
	{
		Task<ICollection<Product>> GetAllAsync();
		Task<Product> TryGetByIdAsync(Guid id);
		Task DeleteAsync(Product product);
		Task AddAsync(Product product);
		Task UpdateAsync(Product product);
	}
}
