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

        public IActionResult Register(string returnUrl)
        {
            return View(new Register() { ReturnUrl = returnUrl});
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
                var user = new User { UserName = register.Email, Email = register.Email };
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
                    {
                        if(error.Code == "PasswordRequiresNonAlphanumeric") 
                            ModelState.AddModelError("", "Пароль должнен содержать хотя бы один небуквенно - цифровой символ.");
                        else if (error.Code == "PasswordTooShort")
                            ModelState.AddModelError("", "Минимальная длина пароля - 6 символов!");
                        else if (error.Code == "PasswordRequiresDigit")
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы одну цифру (от '0' до '9').");
                        else if(error.Code == "PasswordRequiresLower")
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы одну строчную букву ('a'-'z').");
                        else if(error.Code == "PasswordRequiresUpper")
                            ModelState.AddModelError("", "Пароль должен содержать хотя бы одну заглавную букву ('A'-'Z').");
                        else if (error.Code == "DuplicateUserName")
                            ModelState.AddModelError("", $"Имя пользователя «{register.Email}» уже занято.");
                    }
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
