using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp
{
    public class OrdersInMemoryRepository: IOrdersRepository
    {
        private List<Order> orders = new List<Order>();
        public void Add(Order order)
        {
            orders.Add(order);
        }

        public ICollection<Order> GetAll()
        {
            return orders;
        }

        public Order TryGetByOrderId(string guid)
        {
            return orders.FirstOrDefault(order => order.Id.ToString() == guid);
        }

        public Order TryGetByOrderId(Guid id)
        {
            return orders.FirstOrDefault(order => order.Id == id);
        }

        public void UpdateStatus(Guid id, OrderStatus status)
        {
            var order = TryGetByOrderId(id);
            if(order != null)
            {
                order.Status = status;
            }
        }
    }
}
