using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class AddProductViewModel
    {
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
        public IFormFile[] UploadedFiles { get; set; }
    }
}
