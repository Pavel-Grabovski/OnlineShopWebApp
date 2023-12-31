using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewsModels;

public class Register
{
    [Required(ErrorMessage = "Не указан E-mail!")]
    [EmailAddress(ErrorMessage = "Неправильно введен E-mail!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [MinLength(6, ErrorMessage = "Минимальная длина пароля - 6 символов!")]
    [MaxLength(20, ErrorMessage = "Максимальная длина пароля - 20 символов!")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Не указан повторный пароль пароль!")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают!")]
    public string ConfirmPassword { get; set; }
    public string? ReturnUrl { get; set; }
}
