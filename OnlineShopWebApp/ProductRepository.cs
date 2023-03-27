using OnlineShopWebApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWebApp
{
    public class ProductRepository
    {
        private static List<Product> products = new List<Product>()
        {
            new Product("Name1", 10, "Decs1", "/images/image1.jpg"),
            new Product("Name2", 20, "Decs2", "/images/image2.jpg"),
            new Product("Name3", 30, "Decs3", "/images/image3.png"),
            new Product("Name4", 40, "Decs4", "/images/image4.jpg"),
            new Product("Name5", 50, "Decs5", "/images/image5.jpg"),
            new Product("Name6", 60, "Decs6", "/images/image6.jpg"),
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
