namespace OnlineShopWebApp.ViewsModels;

public class CartViewModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public ICollection<CartItemViewModel> Items { get; set; }
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
