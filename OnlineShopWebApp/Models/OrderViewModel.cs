namespace OnlineShopWebApp.Models;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public UserDeliveryInfoViewModel UserInfo { get; set; }
    public List<CartItemViewModel> Items { get; set; }
    public DateTime CreateDataTime { get; set; }
    public OrderStatusViewModel Status { get; set; }
    public decimal Cost
    {
        get => Items.Sum(x => x.Cost);
    }
    public OrderViewModel()
    {
        Id = Guid.NewGuid();
        Status = OrderStatusViewModel.Created;
        CreateDataTime = DateTime.Now;
    }
}
