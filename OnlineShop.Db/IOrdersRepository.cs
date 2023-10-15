using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IOrdersRepository
    {
        void Add(Order order);
        ICollection<Order> GetAll();
        Order TryGetByOrderId(Guid id);
        void UpdateStatus(Guid id, OrderStatus status);
    }
}