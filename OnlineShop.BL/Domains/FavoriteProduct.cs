namespace OnlineShop.BL.Domains;

public class FavoriteProduct
{
	public Guid Id { get; set; }
	public string UserId { get; set; }
	public Product Product { get; set; }
}
