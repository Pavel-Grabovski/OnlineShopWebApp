using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public static class CartsRepository
    {
        private static List<Cart> carts = new List<Cart>();

        internal static void Add(Product product, string userId)
        {
            var exisitingCart = TryGetByUserId(userId);
            if (exisitingCart == null) 
            {
                var newCart = new Cart
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
                carts.Add(newCart);
            }
            else
            {
                var existingCartItem = exisitingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if(existingCartItem != null)
                {
                    existingCartItem.Amount++;
                }
                else
                {
                    exisitingCart.Items.Add(new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Product = product
                    });
                }
            }
        }

        internal static Cart TryGetByUserId(string userId)
        {
            return carts.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
