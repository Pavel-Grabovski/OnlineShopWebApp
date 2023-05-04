using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(int id);
        void Delete(int productId);
        void Add(Product product);
    }
}
