using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class OrdersInMemoryRepository: IOrdersRepository
    {
        private List<Order> orders = new List<Order>();
        public void Add(Order order)
        {
            orders.Add(order);
        }
    }
}
