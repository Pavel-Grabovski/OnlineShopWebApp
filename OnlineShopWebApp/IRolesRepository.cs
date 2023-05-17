using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public interface IRolesRepository
    {
        ICollection<Role> GetAll();
        Role TryGetByName(string name);
        void Delete(Role role);
        void Add(Role role);
        void Delete(string name);
    }
}
