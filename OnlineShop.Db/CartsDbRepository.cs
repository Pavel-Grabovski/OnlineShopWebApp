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

        public void Add(string userId, Product product)
        {
            var cart = TryGetByUserId(userId);
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
                dataBaseContext.Carts.Add(cart);
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
            dataBaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var cart = TryGetByUserId(userId);
            if(cart != null)
            {
                dataBaseContext?.CartItems?.RemoveRange(cart.Items);
                cart?.Items?.Clear();
                dataBaseContext.Carts.Remove(cart);
                dataBaseContext.SaveChanges() ;
            }
        }

        public void DecreaseAmount(string userId, Product product)
        {
            var cart = TryGetByUserId(userId);
            if (cart != null && product != null)
            {
                var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (cartItem != null)
                {
                    cartItem.Amount--;
                    if (cartItem.Amount <= 0)
                    {
                        Remove(userId, product);

                    }
                    dataBaseContext.SaveChanges();
                }
            }

        }

        public void Remove(string userId, Product product)
        {
            var cart = TryGetByUserId(userId);
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

                    dataBaseContext.SaveChanges();
                }
            }
        }

        public void Remove(string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart != null)
            {
                dataBaseContext.Carts.Remove(cart);
                dataBaseContext.SaveChanges();
            }
        }

        public Cart TryGetByUserId(string userId)
        {
            return dataBaseContext.Carts
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .FirstOrDefault(x => x.UserId == userId);
        }
    }
}
