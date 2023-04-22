namespace OnlineShopWebApp.Models
{
    public class Order
    {

        public Guid Id { get; set; }
        public UserInfo UserInfo { get; set; }
        public Cart Cart { get; set; }
        public Order()
        {
            Id = Guid.NewGuid();
        }
    }
}
