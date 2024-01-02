using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces;

public interface IOrdersServices
{
	Task AddAsync(Order order);
	Task<IEnumerable<Order>> GetAllAsync();
	Task<Order> TryGetByOrderIdAsync(Guid id);
	Task UpdateStatusAsync(Guid id, OrderStatus status);
}