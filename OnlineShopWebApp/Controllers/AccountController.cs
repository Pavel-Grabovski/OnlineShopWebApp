using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using Serilog;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersManager usersManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        private readonly ILogger<AccountController> logger;

        public AccountController(IUsersManager usersManager, UserManager<User> userManager, SignInManager<User> singInManager, ILogger<AccountController> logger)
        {
            this.usersManager = usersManager;
            this.userManager = userManager;
            this.signInManager = singInManager;
            this.logger = logger;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new Login() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    if (login.ReturnUrl != null)
                        return Redirect(login.ReturnUrl);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Логин и/или не совпадают!");
                }
            }
            return View(login);
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
                var user = new User { Email = register.Email };
                var result = userManager.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    signInManager.SignInAsync(user, false).Wait();
                    userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", $"Ошибка регистрации в поле {error.Description}");
                }
            }
            return View(register);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
