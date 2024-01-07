namespace OnlineShop.Db.Entities;

public class CartEntity
{
	public Guid Id { get; set; }
	public string Login { get; set; }
	public DateTime CreateDataTime { get; set; } = DateTime.Now;
	public ICollection<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();
}
