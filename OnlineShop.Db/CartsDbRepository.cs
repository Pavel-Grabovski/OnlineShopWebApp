using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class CartsDbRepository : ICartsRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public CartsDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task AddAsync(string userId, Product product)
        {
            var cart = await TryGetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Id = Guid.NewGuid(),
                            Amount = 1,
                            Product= product
                        }
                    }
                };
                await dataBaseContext.Carts.AddAsync(cart);
            }
            else
            {
                var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (cartItem != null)
                {
                    cartItem.Amount++;
                }
                else
                {
                    cartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Product = product
                    };
                    cart.Items.Add(cartItem);
                    dataBaseContext.CartItems.Add(cartItem);

                }
            }
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var cart = await TryGetByUserIdAsync(userId);
            if(cart != null)
            {
                dataBaseContext?.CartItems?.RemoveRange(cart.Items);
                cart?.Items?.Clear();
                dataBaseContext.Carts.Remove(cart);
                await dataBaseContext.SaveChangesAsync() ;
            }
        }

        public async Task DecreaseAmountAsync(string userId, Product product)
        {
            var cart = await TryGetByUserIdAsync(userId);
            if (cart != null && product != null)
            {
                var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (cartItem != null)
                {
                    cartItem.Amount--;
                    if (cartItem.Amount <= 0)
                    {
                        await RemoveAsync(userId, product);

                    }
                    await dataBaseContext.SaveChangesAsync();
                }
            }

        }

        public async Task RemoveAsync(string userId, Product product)
        {
            var cart = await TryGetByUserIdAsync(userId);
            if (cart != null)
            {
                var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (cartItem != null)
                {
                    cart.Items.Remove(cartItem);
                    dataBaseContext.CartItems.Remove(cartItem); 

                    if(cart.Items.Count == 0)
                    {
                        dataBaseContext.Carts.Remove(cart);
                    }

                    await dataBaseContext.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveAsync(string userId)
        {
            var cart = await TryGetByUserIdAsync(userId);
            if (cart != null)
            {
                dataBaseContext.Carts.Remove(cart);
                await dataBaseContext.SaveChangesAsync();
            }
        }

        public async Task<Cart> TryGetByUserIdAsync(string userId)
        {
            return await dataBaseContext.Carts
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
