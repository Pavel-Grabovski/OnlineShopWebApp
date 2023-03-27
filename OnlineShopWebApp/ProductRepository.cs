using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class ProductRepository
    {
        private static List<Product> products = new List<Product>()
        {
            new Product("Name1", 10, "Decs1"),
            new Product("Name2", 20, "Decs2"),
            new Product("Name3", 30, "Decs3"),
            new Product("Name4", 40, "Decs4"),
            new Product("Name5", 50, "Decs5"),
            new Product("Name6", 60, "Decs6"),
        };
        public List<Product> GetAll()
        {
            return products;
        }

        public Product TryGetById(int id)
        {
            return products.FirstOrDefault(product => product.Id == id);
        }
    }
}
