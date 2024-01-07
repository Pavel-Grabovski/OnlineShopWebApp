using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;
using System.Collections.Generic;

namespace OnlineShop.Db.Repositories;

public class FavoritesDbRepository : IFavoriteRepository
{
    private readonly DataBaseContext dataBaseContext;

    public FavoritesDbRepository(DataBaseContext dataBaseContext)
    {
        this.dataBaseContext = dataBaseContext;
    }

    public async Task AddAsync(string login, Guid productId)
    {
        var favoriteProduct = await dataBaseContext.FavoriteProducts
            .FirstOrDefaultAsync(x => x.UserId == login && x.Product.Id == productId);
        if (favoriteProduct != null)
            return;

        var productEntity = await dataBaseContext.Products
            .Include(x => x.Images)
            .FirstOrDefaultAsync(product => product.Id == productId);

        if (productEntity == null)
            return;

        dataBaseContext.FavoriteProducts.Add(new FavoriteProductEntity { Product = productEntity, UserId = login });
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task ClearAsync(string login)
    {
        var favoriteProducts = await dataBaseContext.FavoriteProducts.Where(u => u.UserId == login).ToArrayAsync();
        dataBaseContext.FavoriteProducts.RemoveRange(favoriteProducts);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(string login, Guid productId)
    {
        var favoriteProduct = await dataBaseContext.FavoriteProducts
            .FirstOrDefaultAsync(u => u.UserId == login && u.Product.Id == productId);
        if (favoriteProduct == null)
            return;

        dataBaseContext.FavoriteProducts.Remove(favoriteProduct);
        await dataBaseContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<ProductEntity>> GetAllAsync(string login)
    {
        return await dataBaseContext.FavoriteProducts.Where(x => x.UserId == login)
            .Include(x => x.Product)
            .Select(x => x.Product)
            .ToArrayAsync();
    }
}
