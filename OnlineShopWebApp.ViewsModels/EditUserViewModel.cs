﻿using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp.ViewsModels;

public class EditUserViewModel
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public string Surname { get; set; }
    public string Role { get; set; }
    public string ImagePath { get; set; }
    public IFormFile UploadFile { get; set; }
}
