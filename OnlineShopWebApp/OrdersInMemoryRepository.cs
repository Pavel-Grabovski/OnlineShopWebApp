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

        public ICollection<Order> GetAll()
        {
            return orders;
        }

        public Order TryGetById(string guid)
        {
            return orders.FirstOrDefault(order => order.Id.ToString() == guid);
        }
    }
}
