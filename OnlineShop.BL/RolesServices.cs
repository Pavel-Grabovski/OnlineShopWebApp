using Microsoft.AspNetCore.Identity;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;

namespace OnlineShop.BL
{
    public class RolesServices: IRolesServices
    {
        private readonly RoleManager<IdentityRole> rolesManager;

        public RolesServices(RoleManager<IdentityRole> rolesManager)
        {
            this.rolesManager = rolesManager;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var roles = rolesManager.Roles.Select(r => new Role { Name = r.Name }).ToArray();
            return roles;
        }
    }
}
