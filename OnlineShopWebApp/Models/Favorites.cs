namespace OnlineShopWebApp.Models
{
    public class Favorites
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<FavoriteItem> Items { get; set; }
        public int Amount
        {
            get
            {
                return Items?.Count ?? 0;
            }
        }
    }
}
