using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Не указан E-mail!")]
        [EmailAddress(ErrorMessage = "Неправильно введен E-mail!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(8, ErrorMessage = "Минимальная длина пароля - 8 символов!")]
        [MaxLength(100, ErrorMessage = "Максимальная длина пароля - 100 символов!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан повторный пароль пароль!")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}
