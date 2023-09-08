using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IProductsRepository
    {
        ICollection<Product> GetAll();
        Product TryGetById(Guid id);
        void Delete(Product product);
        void Add(Product product);
        void Update(Product product);
    }
}
