using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.BL;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;
using OnlineShopWebApp.ViewsModels;
namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersServices usersServices;
        private readonly IRolesServices rolesServices;

        public AccountController(IUsersServices usersServices, IRolesServices rolesServices)
        {
            this.usersServices = usersServices;
            this.rolesServices = rolesServices;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await usersServices.PasswordSignInAsync(login.Email, login.Password, login.RememberMe);

                if (result.Succeeded)
                {
                    if (login.ReturnUrl != null)
                        return Redirect(login.ReturnUrl);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Логин и пароль не совпадают!");
                }
            }
            return View(login);
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel register)
        {
            if(register.Email == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }
            if(ModelState.IsValid && register.Password == register.ConfirmPassword)
            {
                var user = new User { Email = register.Email, UserName = register.Email };

                var result = await usersServices.CreateUser(user, register.Password);

                if (result.Succeeded)
                {
                    usersServices.SignInAsync(user, false).Wait();
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

        public async Task<IActionResult> LogoutAsync()
        {
            await usersServices.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Profile()
        {
            var user = usersServices.FindByEmailAsync(User.Identity.Name).Result;
            return View();
        }
        public IActionResult Orders()
        {
            var user = usersServices.FindByEmailAsync(User.Identity.Name).Result;
            return View();
        }
        public IActionResult Settings()
        {
            var user = usersServices.FindByEmailAsync(User.Identity.Name).Result;
            return View();
        }
    }
}
