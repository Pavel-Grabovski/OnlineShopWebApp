using Microsoft.EntityFrameworkCore;
using OnlineShop.Entities;

namespace OnlineShop.Db;

public class OrdersDbRepository : IOrdersRepository
{
    private readonly DataBaseContext dataBaseContext;

    public OrdersDbRepository(DataBaseContext dataBaseContext)
    {
        this.dataBaseContext = dataBaseContext;
    }

    public async Task AddAsync(Order order)
    {
        dataBaseContext.Orders.Add(order);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task<ICollection<Order>> GetAllAsync()
    {
        return await dataBaseContext.Orders
            .Include(x => x.UserInfo)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Images)
            .ToArrayAsync();
    }


    public async Task<Order> TryGetByOrderIdAsync(Guid id)
    {
        return await dataBaseContext.Orders
            .Include(x => x.UserInfo)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateStatusAsync(Guid id, OrderStatus status)
    {
        var order = await TryGetByOrderIdAsync(id);
        if (order != null)
        {
            order.Status = status;
            await dataBaseContext.SaveChangesAsync();
        }
    }
}
