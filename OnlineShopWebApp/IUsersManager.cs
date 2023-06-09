using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IUsersManager
    {
        ICollection<UserAccount> GetAll();
        UserAccount TryGetByUserName(string email);
        void Delete(UserAccount user);
        void Add(UserAccount user);
        void ChangePassword(string email, string password);
    }
}
