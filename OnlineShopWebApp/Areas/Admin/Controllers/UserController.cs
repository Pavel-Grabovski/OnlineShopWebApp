using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> usersManager;

        public UserController(UserManager<User> usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var users = usersManager.Users.ToList(); 
            return View(users.Select(x => x.ToUserViewModel()).ToList());
        }
        public IActionResult Delete(string email)
        {
            var user = usersManager.FindByEmailAsync(email).Result;
            usersManager.DeleteAsync(user).Wait();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(string email)
        {
            var user = usersManager.FindByEmailAsync(email).Result;

            return View(user.ToUserViewModel());
        }

        public IActionResult Save(UserViewModel newUserAccount)
        {
            //var userAccounts = usersManager.TryGetByUserName(newUserAccount.Email);
            //usersManager.Delete(userAccounts);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangePassword(string email)
        {
            var changePassword = new ChangePasswordViewModel()
            {
                Email = email
            };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (changePassword.Email == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                var user = usersManager.FindByEmailAsync(changePassword.Email).Result;
                var newHashPassword = usersManager.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newHashPassword;
                usersManager.UpdateAsync(user).Wait();
                return RedirectToAction(nameof(Index));
            }
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            var user = usersManager.FindByEmailAsync(userViewModel.Email).Result;
            user.PhoneNumber = userViewModel.Phone;
            user.Name = userViewModel.Name;
            user.Surname = userViewModel.Surname;
            user.Patronymic = userViewModel.Patronymic;
            usersManager.UpdateAsync(user).Wait();

            return View(nameof(Details), userViewModel);
        }
    }
}
