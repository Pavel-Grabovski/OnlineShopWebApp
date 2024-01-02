using OnlineShop.Db.Entities;

namespace OnlineShop.Db.Interfaces
{
	public interface IProductsRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<ProductEntity> TryGetByIdAsync(Guid id);
        Task DeleteAsync(ProductEntity product);
        Task DeleteAsync(Guid id);
        Task AddAsync(ProductEntity product);
        Task UpdateAsync(ProductEntity product);
    }
}
