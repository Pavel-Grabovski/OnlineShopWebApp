using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces;

public interface ICartsServices
{
	Task AddAsync(string login, Guid productId);
	Task<Cart> TryGetByLoginAsync(string login);
	Task DecreaseAmountAsync(string login, Guid productId);
	Task ClearAsync(string login);
    Task RemoveAsync(string login, Guid productId);
}
