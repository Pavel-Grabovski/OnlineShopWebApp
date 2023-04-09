namespace OnlineShopWebApp.Models
{
    public class Favorites
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<FavoriteItem> Items { get; set; }
    }
}
