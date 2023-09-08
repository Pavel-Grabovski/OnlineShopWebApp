using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DataBaseContext: DbContext
    {
        // Доступ к таблицам
        public DbSet<Product> Products { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
