﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineShopWebApp.ViewsModels;

public class UserDeliveryInfoViewModel
{
    public Guid Id { get; set; }
    [ValidateNever]

    [EmailAddress(ErrorMessage = "Укажите ваш e-mail.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не указано имя.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Не указана фамилия.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Не указано отчество.")]
    public string Patronymic { get; set; }

    [Required(ErrorMessage = "Не указан номер телефона.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Не указан адрес доставки.")]
    public string Address { get; set; }
}
