using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Product
    {
        private static int instanceCounter = 0;
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя.")]
        [MinLength(2, ErrorMessage = "Короткое имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано стоимость.")]
        [Range(0, int.MaxValue, ErrorMessage = "Некоректная стоимость")]
        [DataType(DataType.Currency, ErrorMessage = "Введите число")]
        public decimal Cost { get; set; }

        [ValidateNever]
        public string Description { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

        public Product(string name, decimal cost, string description, string imagePath) : this()  
        {
            Name = name;
            Cost = cost;
            Description = description;
            ImagePath = imagePath;
        }

        public Product()
        {
            Id = instanceCounter;
            instanceCounter++;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}";
        }
    }
}
