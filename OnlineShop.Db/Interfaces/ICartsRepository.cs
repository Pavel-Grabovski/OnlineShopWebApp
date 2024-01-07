using OnlineShop.Db.Entities;

namespace OnlineShop.Db.Interfaces
{
	public interface ICartsRepository
    {
        Task AddAsync(string login, Guid productId);
        Task<CartEntity> TryGetByLoginAsync(string login);
        Task DecreaseAmountAsync(string login, Guid productId);
        Task RemoveAsync(string login, Guid productId);
        Task ClearAsync(string login);
    }
}
