using OnlineShop.Db.Entities;

namespace OnlineShop.Db.Interfaces
{
	public interface IOrdersRepository
    {
        Task AddAsync(OrderEntity order);
        Task<ICollection<OrderEntity>> GetAllAsync();
        Task<OrderEntity> TryGetByOrderIdAsync(Guid id);
        Task UpdateStatusAsync(Guid id, OrderStatusEntity status);
    }
}