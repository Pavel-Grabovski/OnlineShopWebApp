namespace OnlineShop.Db.Entities;

public class ProductEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public decimal Cost { get; set; }
	public string? Description { get; set; }
	public List<ImageEntity> Images { get; set; }
}
