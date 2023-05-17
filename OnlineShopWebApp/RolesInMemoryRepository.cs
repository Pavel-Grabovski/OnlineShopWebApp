using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class RolesInMemoryRepository : IRolesRepository
    {
        private readonly ICollection<Role> roles = new List<Role>();
        public void Add(Role role)
        {
            if(roles.Contains(role)) return;
            roles.Add(role);
        }

        public void Delete(Role role)
        {
            roles.Remove(role);
        }

        public void Delete(string name)
        {
            var role = roles.FirstOrDefault(x => x.Name == name);
            Delete(role);
        }

        public ICollection<Role> GetAll()
        {
            return roles;
        }

        public Role TryGetByName(string name)
        {
            return roles.FirstOrDefault(x => x.Name == name);
        }
    }
}
