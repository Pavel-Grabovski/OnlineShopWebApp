using Microsoft.EntityFrameworkCore;

using OnlineShop.Db;
using OnlineShop.Entities;

namespace OnlineShopWebApp;

public class ProductsDbRepository : IProductsRepository
{
    private readonly DataBaseContext dataBaseContext;

    public ProductsDbRepository(DataBaseContext dataBaseContext)
    {
        this.dataBaseContext = dataBaseContext;
    }

    public async Task AddAsync(Product product)
    {
        dataBaseContext.Products.Add(product);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        dataBaseContext.Products.Remove(product);
        await dataBaseContext.SaveChangesAsync();
    }

    public async Task<ICollection<Product>> GetAllAsync()
    {
        return await dataBaseContext.Products.Include(x => x.Images).ToArrayAsync();
    }

    public async Task<Product> TryGetByIdAsync(Guid id)
    {
        return await dataBaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task UpdateAsync(Product product)
    {
        var existingProduct = await dataBaseContext.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;

            if(product.Images != null &&  product.Images.Count > 0)
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
