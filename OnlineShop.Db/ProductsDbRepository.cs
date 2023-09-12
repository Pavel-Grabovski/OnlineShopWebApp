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
            //product.ImagePath = "/images/image1.jpg";
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
            return dataBaseContext.Products.ToList();
        }

        public Product TryGetById(Guid id)
        {
            return dataBaseContext.Products.FirstOrDefault(product => product.Id == id);
        }

        public void Update(Product product)
        {
            var existingProduct = dataBaseContext.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Cost = product.Cost;
                existingProduct.ImagePath = "/images/image1.jpg";
                dataBaseContext.SaveChanges();
            }
        }
    }
}
