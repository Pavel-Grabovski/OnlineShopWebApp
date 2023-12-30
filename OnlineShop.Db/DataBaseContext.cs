using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;

namespace OnlineShop.Db
{
	public class DataBaseContext : DbContext
    {
        // Доступ к таблицам
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image> Images { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            var product1Id = Guid.Parse("5db89dfd-0615-453b-b9af-b39835d33c90");
            var product2Id = Guid.Parse("7d7e9765-9be5-4c3d-8ca6-868dfa887543");

            var image1 = new Image
            {
                Id = Guid.Parse("475bffd1-7f4a-4dd1-a327-5f3dd48321b8"),
                Url = "/images/image1.jpg",
                ProductId = product1Id
            };

            var image2 = new Image
            {
                Id = Guid.Parse("29e429d0-a84b-42e7-ad8b-75f7e538fcb3"),
                Url = "/images/image2.jpg",
                ProductId = product2Id
            };

            modelBuilder.Entity<Image>().HasData(image1, image2);

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product
                {
                    Id = product1Id,
                    Name = "Тестовый продукт1",
                    Cost = 1,
                    Description = "Тест описания1",
                },
                new Product
                {
                    Id = product2Id,
                    Name = "Тестовый продукт2",
                    Cost = 2,
                    Description = "Тест описания2",
                },

            });
        }
    }
}
