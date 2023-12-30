namespace OnlineShop.Db.Entities;

public class Cart
{
	public Guid Id { get; set; }
	public string UserId { get; set; }
	public DateTime CreateDataTime { get; set; } = DateTime.Now;
	public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
