using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces
{
    public interface IRolesServices
    {
        IEnumerable<Role> GetAllRoles();
    }
}
