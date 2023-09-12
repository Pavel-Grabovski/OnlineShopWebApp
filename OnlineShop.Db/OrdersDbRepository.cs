using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public OrdersDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(Order order)
        {
            dataBaseContext.Orders.Add(order);
            dataBaseContext.SaveChanges();
        }

        public ICollection<Order> GetAll()
        {
            return dataBaseContext.Orders
                .Include(x => x.UserInfo)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product).ToList();
        }


        public Order TryGetByOrderId(Guid id)
        {
            return dataBaseContext.Orders
                .Include(x => x.UserInfo)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public void UpdateStatus(Guid id, OrderStatus status)
        {
            var order = TryGetByOrderId(id);
            if (order != null)
            {
                order.Status = status;
                dataBaseContext.SaveChanges();
            }
        }
    }
}
