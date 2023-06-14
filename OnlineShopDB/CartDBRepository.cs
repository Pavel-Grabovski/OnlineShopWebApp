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
        public List<CartItem> GetAll(Guid cartId)
        {
            return dataBaseContext.CartItems.Where(x => x.CartId == cartId).ToList();
        }
        public void Add(Product product, string userId)
        {
            var сart = TryGetByUserId(userId);
            if (сart == null)
            {
                сart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };
                dataBaseContext.Carts.Add(сart);
            }

            var cartItems = dataBaseContext.CartItems.Where(x => x.CartId == сart.Id).ToArray();
            var cartItem = cartItems.FirstOrDefault(x => x.ProductId == product.Id);

            if (cartItem != null)
            {
                cartItem.Amount++;
            }
            else
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Amount = 1,
                    CartId = сart.Id
                };
                dataBaseContext.Add(cartItem);
            }
            dataBaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var cart = dataBaseContext.Carts.FirstOrDefault(x => x.UserId == userId);
            if(cart != null)
            {
                var cartItems = dataBaseContext.CartItems.Where(x => x.CartId == cart.Id).ToArray();
                foreach(var item in cartItems)
                {
                    dataBaseContext.CartItems.Remove(item);
                }
                dataBaseContext.Carts.Remove(cart);
            }
            dataBaseContext.SaveChanges();
        }

        public void DecreaseAmount(Guid cartItemId, string userId)
        {
            var cartItem = dataBaseContext.CartItems.FirstOrDefault(x => x.Id == cartItemId);
            var cart = dataBaseContext.Carts.FirstOrDefault(x => x.UserId == userId);

            if (cart != null && cartItem != null && cartItem.CartId == cart.Id)
            {
                if(cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                }
                else
                {
                    dataBaseContext.CartItems.Remove(cartItem);
                }
                dataBaseContext.SaveChanges();
            }
        }

        public Cart TryGetByUserId(string userId)
        {
            return dataBaseContext.Carts.FirstOrDefault(x => x.UserId == userId);
        }

        public CartItem GetCartItem(Guid cartItemId)
        {
            return dataBaseContext.CartItems.FirstOrDefault(x => x.Id == cartItemId);
        }

        public void Remove(Guid cartItemId, string userId)
        {
            var cartItem = dataBaseContext.CartItems.FirstOrDefault(x => x.Id == cartItemId);
            var cart = dataBaseContext.Carts.FirstOrDefault(x => x.UserId == userId);

            if (cartItem != null && cartItem.CartId == cart.Id)
            {
                dataBaseContext.CartItems.Remove(cartItem);
                dataBaseContext.SaveChanges();
            }
        }
    }
}
