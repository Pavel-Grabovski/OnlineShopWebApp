using Microsoft.EntityFrameworkCore;
using OnlineShopDB.Models;

namespace OnlineShopDB
{
    public class CartDBRepository : ICartsRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public CartDBRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public ICollection<CartItem> GetAll(string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart != null)
            {
                return cart.CartItems;
            }
            return null;
        }
        public void Add(Product product, string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };
                cart.CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Amount = 1
                    }
                };
                dataBaseContext.Carts.Add(cart);
            }
            else
            {
                var duplCartItem = cart.CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
                if (duplCartItem != null)
                {
                    duplCartItem.Amount++;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Amount = 1
                    };
                    cart.CartItems.Add(cartItem);
                    dataBaseContext.CartItems.Add(cartItem);
                }
            }

            dataBaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var cart = TryGetByUserId(userId);
            if (cart != null)
            {
                var cartItems = cart.CartItems;
                foreach (var item in cartItems)
                {
                    dataBaseContext.CartItems.Remove(item);
                }
                cart.CartItems?.Clear();
                dataBaseContext.Carts.Remove(cart);
            }
            dataBaseContext.SaveChanges();
        }

        public void DecreaseAmount(string userId, Guid productId)
        {
            var cart = TryGetByUserId(userId);

            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
                if (cartItem != null)
                {
                    cartItem.Amount--;
                    if (cartItem.Amount == 0)
                    {
                        Remove(userId, productId);
                    }
                    dataBaseContext.SaveChanges();
                }
            }
        }

        public Cart TryGetByUserId(string userId)
        {
            return dataBaseContext.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }

        public void Remove(string userId, Guid productId)
        {
            var cart = TryGetByUserId(userId);
            if(cart!= null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
                if(cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                    dataBaseContext.CartItems.Remove(cartItem);
                }
            }

            dataBaseContext.SaveChanges();
        }
    }
}
