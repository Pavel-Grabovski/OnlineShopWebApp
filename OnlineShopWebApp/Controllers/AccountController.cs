using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShopWebApp.Models;
using Serilog;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersManager usersManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(IUsersManager usersManager, ILogger<AccountController> logger)
        {
            this.usersManager = usersManager;
            this.logger = logger; 
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if(!ModelState.IsValid)
                return RedirectToAction(nameof(Login));

            var userAccount = usersManager.TryGetByUserName(login.Email);
            if (userAccount == null || userAccount.Password != login.Password)
            {
                ModelState.AddModelError("", "Неверный логин или пароль!");
                return View(login);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if(register.Email == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }
            if(ModelState.IsValid && register.Password == register.ConfirmPassword)
            {
                usersManager.Add(new UserAccount
                {
                    Email = register.Email,
                    Password = register.Password
                });
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(register);
        }
    }
}
