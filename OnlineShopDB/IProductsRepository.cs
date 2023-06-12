using OnlineShopDB.Models;

namespace OnlineShopDB
{
    public interface IProductsRepository
    {
        ICollection<Product> GetAll();
        Product TryGetById(Guid id);
        void Delete(Product product);
        void Add(Product product);
        void Edit(Product product);
    }
}
