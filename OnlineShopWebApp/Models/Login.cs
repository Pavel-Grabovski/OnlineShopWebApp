﻿using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [BindProperty]
        public bool RememberMe{ get; set; }
    }
}
