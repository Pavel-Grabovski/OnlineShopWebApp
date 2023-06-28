using System.Data;

namespace OnlineShopDB.Models
{
    public class Cart
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
