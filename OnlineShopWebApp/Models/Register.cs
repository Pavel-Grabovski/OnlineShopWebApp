using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Не указан E-mail")]
        [EmailAddress(ErrorMessage = "Неправильно введен E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан повторный пароль пароль")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
