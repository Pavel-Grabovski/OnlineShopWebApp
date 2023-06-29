using Microsoft.EntityFrameworkCore;
using OnlineShopDB;
using OnlineShopDB.Models;

namespace OnlineShopWebApp
{
    public class FavoriteDBRepository : IFavoritesRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public FavoriteDBRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(string userId, Product product)
        {
            var existingProduct  = dataBaseContext.FavoriteProducts.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            if(existingProduct == null)
            {
                var favoriteProduct = new FavoriteProduct
                {
                    Product = product,
                    UserId = userId
                };
                dataBaseContext.FavoriteProducts.Add(favoriteProduct);
                dataBaseContext.SaveChanges();
            }
        }

        public void Clear(string userId)
        {
            var delFavoriteProducts = dataBaseContext.FavoriteProducts.Where(x => x.UserId == userId);
            dataBaseContext.FavoriteProducts.RemoveRange(delFavoriteProducts);
            dataBaseContext.SaveChanges();
        }

        public ICollection<Product> GetAll(string userId)
        {
            return dataBaseContext.FavoriteProducts.Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .Select(x => x.Product)
                .ToList();
        }

        public void Remove(string userId, Product product)
        {
            var existingProduct = dataBaseContext.FavoriteProducts.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct != null)
            {
                dataBaseContext.FavoriteProducts.Remove(existingProduct);
                dataBaseContext.SaveChanges();
            }
        }
    }
}
