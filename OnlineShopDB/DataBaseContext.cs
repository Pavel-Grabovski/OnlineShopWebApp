using Microsoft.EntityFrameworkCore;
using OnlineShopDB.Models;

namespace OnlineShopDB
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