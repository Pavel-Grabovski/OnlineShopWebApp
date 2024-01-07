using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;

namespace OnlineShop.Db
{
	public class IdentityContext : IdentityDbContext<UserEntity>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}