namespace OnlineShop.Db.Entities;

public class Order
{
	public Guid Id { get; set; }
	public UserDeliveryInfo UserInfo { get; set; }
	public ICollection<CartItem> Items { get; set; }
	public DateTime CreateDataTime { get; set; }
	public OrderStatus Status { get; set; }
	public Order()
	{
		Status = OrderStatus.Created;
		CreateDataTime = DateTime.Now;
	}
}
