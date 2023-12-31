namespace OnlineShop.Db.Entities;

public class FavoriteProductEntity
{
	public Guid Id { get; set; }
	public string UserId { get; set; }
	public ProductEntity Product { get; set; }
}
