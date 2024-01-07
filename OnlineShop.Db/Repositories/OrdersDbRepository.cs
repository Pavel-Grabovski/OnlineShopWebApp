using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories;

	public class OrdersDbRepository : IOrdersRepository
{
    private readonly DataBaseContext dataBaseContext;

    public OrdersDbRepository(DataBaseContext dataBaseContext)
    {
        this.dataBaseContext = dataBaseContext;
    }

    public async Task AddAsync(OrderEntity order)
    {
        dataBaseContext.Orders.Add(order);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task<ICollection<OrderEntity>> GetAllAsync()
    {
        return await dataBaseContext.Orders
            .Include(x => x.UserInfo)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Images)
            .ToArrayAsync();
    }


    public async Task<OrderEntity> TryGetByOrderIdAsync(Guid id)
    {
        return await dataBaseContext.Orders
            .Include(x => x.UserInfo)
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateStatusAsync(Guid id, OrderStatusEntity status)
    {
        var order = await TryGetByOrderIdAsync(id);
        if (order != null)
        {
            order.Status = status;
            await dataBaseContext.SaveChangesAsync();
        }
    }
}
