using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class FavoritesDbRepository : IFavoriteRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public FavoritesDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(string userId, Product product)
        {
            var favoriteProduct = dataBaseContext.FavoriteProducts
                .FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            if(favoriteProduct == null)
            {
                dataBaseContext.FavoriteProducts.Add(new FavoriteProduct { Product =  product, UserId = userId });
                dataBaseContext.SaveChanges();
            }
        }

        public void Clear(string userId)
        {
            var favoriteProducts = dataBaseContext.FavoriteProducts.Where(u => u.UserId == userId).ToList();
            dataBaseContext.FavoriteProducts.RemoveRange(favoriteProducts);
            dataBaseContext.SaveChanges();
        }

        public void Remove(string userId, Product product)
        {
            var favoriteProduct = dataBaseContext
                .FavoriteProducts
                .FirstOrDefault(u => u.UserId == userId && u.Product.Id == product.Id);
            if(favoriteProduct != null)
            {
                dataBaseContext .FavoriteProducts.Remove(favoriteProduct);
                dataBaseContext.SaveChanges();
            }
        }
        public ICollection<Product> GetAll(string userId)
        {
            return dataBaseContext.FavoriteProducts.Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .Select(x => x.Product)
                .ToList();
        }
    }
}
