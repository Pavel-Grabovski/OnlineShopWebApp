using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories;

	public class CartsDbRepository : ICartsRepository
{
    private readonly DataBaseContext dataBaseContext;

    public CartsDbRepository(DataBaseContext dataBaseContext)
    {
        this.dataBaseContext = dataBaseContext;
    }

    public async Task AddAsync(string login, Guid productId)
    {
        var productEntity = await dataBaseContext.Products
            .Include(x => x.Images)
            .FirstOrDefaultAsync(product => product.Id == productId);
        if (productEntity == null)
            return;

        var cart = await TryGetByLoginAsync(login);

        if (cart == null)
        {
            cart = new CartEntity
            {
                Id = Guid.NewGuid(),
                Login = login,
                Items = new List<CartItemEntity>
                {
                    new CartItemEntity
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Product = productEntity
                    }
                }
            };
            await dataBaseContext.Carts.AddAsync(cart);
        }
        else
        {
            var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (cartItem != null)
            {
                cartItem.Amount++;
            }
            else
            {
                cartItem = new CartItemEntity
                {
                    Id = Guid.NewGuid(),
                    Amount = 1,
                    Product = productEntity
                };
                cart.Items.Add(cartItem);
                dataBaseContext.CartItems.Add(cartItem);

            }
        }
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task ClearAsync(string userId)
    {
        var cart = await TryGetByLoginAsync(userId);
        if (cart != null)
        {
            dataBaseContext?.CartItems?.RemoveRange(cart.Items);
            cart?.Items?.Clear();
            dataBaseContext.Carts.Remove(cart);
            await dataBaseContext.SaveChangesAsync();
        }
    }

    public async Task DecreaseAmountAsync(string userId, Guid productId)
    {
        var productEntity = await dataBaseContext.Products
             .Include(x => x.Images)
             .FirstOrDefaultAsync(product => product.Id == productId);
        if (productEntity == null)
            return;

        var cart = await TryGetByLoginAsync(userId);
        if (cart != null && productEntity != null)
        {
            var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == productEntity.Id);
            if (cartItem != null)
            {
                cartItem.Amount--;
                if (cartItem.Amount <= 0)
                {
                    await RemoveAsync(userId, productId);

                }
                await dataBaseContext.SaveChangesAsync();
            }
        }

    }

    public async Task RemoveAsync(string userId, Guid productId)
    {
        var productEntity = await dataBaseContext.Products
             .Include(x => x.Images)
             .FirstOrDefaultAsync(product => product.Id == productId);
        if (productEntity == null)
            return;

        var cart = await TryGetByLoginAsync(userId);
        if (cart != null)
        {
            var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == productEntity.Id);
            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                dataBaseContext.CartItems.Remove(cartItem);

                if (cart.Items.Count == 0)
                {
                    dataBaseContext.Carts.Remove(cart);
                }

                await dataBaseContext.SaveChangesAsync();
            }
        }
    }

    public async Task RemoveAsync(string userId)
    {
        var cart = await TryGetByLoginAsync(userId);
        if (cart != null)
        {
            dataBaseContext.Carts.Remove(cart);
            await dataBaseContext.SaveChangesAsync();
        }
    }

    public async Task<CartEntity> TryGetByLoginAsync(string userId)
    {
        return await dataBaseContext.Carts
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Images)
            .FirstOrDefaultAsync(x => x.Login == userId);
    }
}
