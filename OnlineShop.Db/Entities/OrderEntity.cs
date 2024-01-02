namespace OnlineShop.Db.Entities;

public class OrderEntity
{
	public Guid Id { get; set; }
	public UserDeliveryInfoEntity UserInfo { get; set; }
	public ICollection<CartItemEntity> Items { get; set; }
	public DateTime CreateDataTime { get; set; }
	public OrderStatusEntity Status { get; set; }
	public OrderEntity()
	{
		Status = OrderStatusEntity.Created;
		CreateDataTime = DateTime.Now;
	}
}
