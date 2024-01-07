using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Entities;

namespace OnlineShop.Db
{
	public class DataBaseContext : DbContext
    {
        // Доступ к таблицам
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<FavoriteProductEntity> FavoriteProducts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageEntity>().HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            var product1Id = Guid.Parse("5db89dfd-0615-453b-b9af-b39835d33c90");
            var product2Id = Guid.Parse("7d7e9765-9be5-4c3d-8ca6-868dfa887543");

            var image1 = new ImageEntity
            {
                Id = Guid.Parse("475bffd1-7f4a-4dd1-a327-5f3dd48321b8"),
                Url = "/images/image1.jpg",
                ProductId = product1Id
            };

            var image2 = new ImageEntity
            {
                Id = Guid.Parse("29e429d0-a84b-42e7-ad8b-75f7e538fcb3"),
                Url = "/images/image2.jpg",
                ProductId = product2Id
            };

            modelBuilder.Entity<ImageEntity>().HasData(image1, image2);

            modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity[]
            {
                new ProductEntity
                {
                    Id = product1Id,
                    Name = "Тестовый продукт1",
                    Cost = 1,
                    Description = "Тест описания1",
                },
                new ProductEntity
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
