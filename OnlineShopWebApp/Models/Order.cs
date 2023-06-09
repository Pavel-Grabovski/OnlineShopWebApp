using System;

namespace OnlineShopWebApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public UserDeliveryInfo UserInfo { get; set; }
        public Cart Cart { get; set; }
        public DateTime DataTime { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Cost
        {
            get => Cart.Cost;
        }
        public Order()
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Created;
            DataTime = DateTime.Now;
        }
    }
}
