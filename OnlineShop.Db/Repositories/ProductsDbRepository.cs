using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories;

	public class ProductsDbRepository : IProductsRepository
{
    private readonly DataBaseContext dataBaseContext;

    public ProductsDbRepository(DataBaseContext dataBaseContext)
    {
        this.dataBaseContext = dataBaseContext;
    }

    public async Task AddAsync(ProductEntity product)
    {
        dataBaseContext.Products.Add(product);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProductEntity product)
    {
        dataBaseContext.Products.Remove(product);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingProduct = await dataBaseContext.Products
            .Include(product => product.Images)
            .FirstOrDefaultAsync(product => product.Id == id);

        dataBaseContext.Products.Remove(existingProduct);
        await dataBaseContext.SaveChangesAsync(); 
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return await dataBaseContext.Products.Include(x => x.Images).ToArrayAsync();
    }

    public async Task<ProductEntity> TryGetByIdAsync(Guid id)
    {
        return await dataBaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task UpdateAsync(ProductEntity product)
    {
        var existingProduct = await dataBaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;

            if (product.Images != null && product.Images.Count > 0)
            {
                foreach (var image in product.Images)
                {
                    image.ProductId = product.Id;
                    dataBaseContext.Images.Add(image);
                }

            }

            await dataBaseContext.SaveChangesAsync();
        }
    }
}
