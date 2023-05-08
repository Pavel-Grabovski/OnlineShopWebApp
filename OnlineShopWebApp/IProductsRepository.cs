using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(int id);
        void Delete(Product product);
        void Add(Product product);
    }
}
