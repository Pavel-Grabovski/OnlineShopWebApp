﻿using Microsoft.AspNetCore.Identity;

namespace OnlineShop.BL.Domains;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public string Surname { get; set; }
    public string ImagePath { get; set; }
}
