namespace OnlineShop.Db.Entities;

public class ImageEntity
{
	public Guid Id { get; set; }
	public string Url { get; set; }
	public Guid ProductId { get; set; }
	public ProductEntity Product { get; set; }
}
