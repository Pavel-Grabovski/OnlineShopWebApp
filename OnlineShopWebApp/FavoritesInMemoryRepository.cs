using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class FavoritesInMemoryRepository : IFavoritesRepository
    {
        private List<Favorites> favorites = new List<Favorites>();
        public void Add(Product product, string userId)
        {
            var exisitingFavorites = TryGetByUserId(userId);
            if(exisitingFavorites == null)
            {
                var newFavorite = new Favorites
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<FavoriteItem>
                    {
                        new FavoriteItem
                        {
                            Id = Guid.NewGuid(),
                            Product = product,
                        }
                    }
                };
                favorites.Add(newFavorite);
            }
            else
            {
                var exisitingFavoriteItems = exisitingFavorites.Items.FirstOrDefault(x =>x.Product.Id == product.Id);
                if (exisitingFavoriteItems == null)
                {
                    exisitingFavorites.Items.Add(new FavoriteItem
                    {
                        Id = Guid.NewGuid(),
                        Product = product
                    });
                }
                
            }
        }

        public void Clear(string userId)
        {
            var exisitingFavorites = TryGetByUserId(userId);
            if (exisitingFavorites != null)
            {
                favorites.Remove(exisitingFavorites);
            }
        }

        public void Remove(Product product, string userId)
        {
            var exisitingFavorites = TryGetByUserId(userId);
            if( exisitingFavorites != null)
            {
                var exisitingFavoriteItems = exisitingFavorites.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (exisitingFavoriteItems != null)
                {
                    exisitingFavorites.Items.Remove(exisitingFavoriteItems);
                }
            }
        }

        public Favorites TryGetByUserId(string userId)
        {
            return favorites.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
