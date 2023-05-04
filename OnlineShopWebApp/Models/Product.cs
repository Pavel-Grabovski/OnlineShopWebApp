namespace OnlineShopWebApp.Models
{
    public class Product
    {
        private static int instanceCounter = 0;
        public int Id { get; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public Product(string name, decimal cost, string description, string imagePath)
        {
            Id = instanceCounter;
            Name = name;
            Cost = cost;
            Description = description;
            instanceCounter++;
            ImagePath = imagePath;
        }

        public Product()
        {
            Id = instanceCounter;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}";
        }
    }
}
