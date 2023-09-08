using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class CartsInMemoryRepository: ICartsRepository
    {
        private List<Cart> carts = new List<Cart>();

        public void Add(ProductViewModel product, string userId)
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

        public void Clear(string userId)
        {
            var exisitingCart = TryGetByUserId(userId);
            carts.Remove(exisitingCart);
        }

        public void DecreaseAmount(ProductViewModel product, string userId)
        {
            var exisitingCart = TryGetByUserId(userId);
            if(exisitingCart != null)
            {
                var existingCartItem = exisitingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingCartItem != null )
                {
                    if(existingCartItem.Amount > 1) 
                    {
                        existingCartItem.Amount--;
                    }
                    else
                    {
                        exisitingCart.Items.Remove(existingCartItem);
                    }
                }
            }

        }

        public void Remove(ProductViewModel product, string userId)
        {
            var exisitingCart = TryGetByUserId(userId);
            if (exisitingCart != null)
            {
                var existingCartItem = exisitingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingCartItem != null)
                {
                    exisitingCart.Items.Remove(existingCartItem);
                }
            }
        }

        public Cart TryGetByUserId(string userId)
        {
            return carts.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
