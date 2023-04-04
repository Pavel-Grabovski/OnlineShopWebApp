using OnlineShopWebApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWebApp
{
    public class ProductsInMemoryRepository : IProductsRepository
    {
        private List<Product> products = new List<Product>()
        {
            new Product("Спартанский шлем", 10, "Decs1", "/images/image1.jpg"),
            new Product("Винтовка M-16", 20, "Decs2", "/images/image2.jpg"),
            new Product("Боксерские перчатки", 30, "Decs3", "/images/image3.png"),
            new Product("Меч", 40, "Decs4", "/images/image4.jpg"),
            new Product("Плов", 50, "Decs5", "/images/image5.jpg"),
            new Product("Лев", 60, "Decs6", "/images/image6.jpg"),
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
