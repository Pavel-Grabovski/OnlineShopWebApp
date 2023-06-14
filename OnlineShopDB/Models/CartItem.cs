namespace OnlineShopDB.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public Guid CartId { get; set; }
    }
}
