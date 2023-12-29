using OnlineShop.Entities;

namespace OnlineShop.Db;

public interface IOrdersRepository
{
    Task AddAsync(Order order);
    Task<ICollection<Order>> GetAllAsync();
    Task<Order> TryGetByOrderIdAsync(Guid id);
    Task UpdateStatusAsync(Guid id, OrderStatus status);
}