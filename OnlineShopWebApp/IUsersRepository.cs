using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IUsersRepository
    {
        ICollection<UserInfo> GetAll();
        Product TryGetByUserId(int id);
        void Delete(Product product);
        void Add(Product product);
    }
}
