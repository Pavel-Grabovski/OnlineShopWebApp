using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public ProductsDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(Product product)
        {
            dataBaseContext.Products.Add(product);
            dataBaseContext.SaveChanges();
        }

        public void Delete(Product product)
        {
            dataBaseContext.Products.Remove(product);
            dataBaseContext.SaveChanges();
        }

        public ICollection<Product> GetAll()
        {
            return dataBaseContext.Products.Include(x => x.Images).ToList();
        }

        public Product TryGetById(Guid id)
        {
            return dataBaseContext.Products.Include(x => x.Images).FirstOrDefault(product => product.Id == id);
        }

        public void Update(Product product)
        {
            var existingProduct = dataBaseContext.Products.Include(x => x.Images).FirstOrDefault(x => x.Id == product.Id);
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

                dataBaseContext.SaveChanges();
            }
        }
    }
}
