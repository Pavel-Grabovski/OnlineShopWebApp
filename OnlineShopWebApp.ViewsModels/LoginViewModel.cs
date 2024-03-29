﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewsModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не указан E-mail")]
    [EmailAddress(ErrorMessage = "Неправильно введен E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}
