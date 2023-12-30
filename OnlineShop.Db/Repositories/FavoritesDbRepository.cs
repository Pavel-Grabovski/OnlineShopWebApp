using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories
{
	public class FavoritesDbRepository : IFavoriteRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public FavoritesDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task AddAsync(string userId, Product product)
        {
            var favoriteProduct = await dataBaseContext.FavoriteProducts
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            if (favoriteProduct == null)
            {
                dataBaseContext.FavoriteProducts.Add(new FavoriteProduct { Product = product, UserId = userId });
                await dataBaseContext.SaveChangesAsync();
            }
        }

        public async Task ClearAsync(string userId)
        {
            var favoriteProducts = await dataBaseContext.FavoriteProducts.Where(u => u.UserId == userId).ToArrayAsync();
            dataBaseContext.FavoriteProducts.RemoveRange(favoriteProducts);
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(string userId, Product product)
        {
            var favoriteProduct = await dataBaseContext.FavoriteProducts
                .FirstOrDefaultAsync(u => u.UserId == userId && u.Product.Id == product.Id);
            if (favoriteProduct != null)
            {
                dataBaseContext.FavoriteProducts.Remove(favoriteProduct);
                await dataBaseContext.SaveChangesAsync();
            }
        }
        public async Task<ICollection<Product>> GetAllAsync(string userId)
        {
            return await dataBaseContext.FavoriteProducts.Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .Select(x => x.Product)
                .ToArrayAsync();
        }
    }
}
