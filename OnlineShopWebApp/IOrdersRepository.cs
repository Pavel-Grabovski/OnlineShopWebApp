using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IOrdersRepository
    {
        void Add(Order order);
        ICollection<Order> GetAll();
        Order TryGetByOrderId(string guid);
        Order TryGetByOrderId(Guid id);
        void UpdateStatus(Guid id, OrderStatus status);
    }
}