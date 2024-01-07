namespace OnlineShop.BL.Domains;

public class Cart
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public DateTime CreateDataTime { get; set; } = DateTime.Now;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    public decimal Cost
    {
        get
        {
            return Items?.Sum(cartItem => cartItem.Cost) ?? 0;
        }
    }
    public int Amount
    {
        get
        {
            return Items?.Sum(cartItem => cartItem.Amount) ?? 0;
        }
    }
}
