namespace OnlineShopWebApp.Models
{
    public class Products
    {
        private int instanceCounter = 0;
        public int Id { get; }
        public string Name { get; }
        public decimal Cost { get; }
        public string Description { get; }

        public Products(string name, decimal cost, string description)
        {
            Id = instanceCounter;
            Name = name;
            Cost = cost;
            Description = description;
            instanceCounter++;
        }
    }
}
