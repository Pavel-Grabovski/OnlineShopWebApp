namespace OnlineShop.Db.Entities;

public class CartItemEntity
{
	public Guid Id { get; set; }
	public ProductEntity Product { get; set; }
	public int Amount { get; set; }
}
