using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
       private readonly IUsersManager usersManager;

        public UserController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var userAccounts = usersManager.GetAll();
            return View(userAccounts);
        }
        public IActionResult Delete(string email)
        {
            var userAccounts = usersManager.TryGetByUserName(email);
            usersManager.Delete(userAccounts);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(string email)
        {
            var userAccounts = usersManager.TryGetByUserName(email);
            return View(userAccounts);
        }

        public IActionResult Save(UserAccount newUserAccount)
        {
            var userAccounts = usersManager.TryGetByUserName(newUserAccount.Email);
            usersManager.Delete(userAccounts);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangePassword(string email)
        {
            var changePassword = new ChangePassword()
            {
                Email = email
            };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (changePassword.Email == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                usersManager.ChangePassword(changePassword.Email, changePassword.Password);
                var user = usersManager.TryGetByUserName(changePassword.Email);
                return View(nameof(Details), user);
            }
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult Edit(UserAccount user)
        {
            var editUser = usersManager.TryGetByUserName(user.Email);

            editUser.Surname = user.Surname;
            editUser.Name = user.Name;
            editUser.Patronymic = user.Patronymic;
            editUser.Phone = user.Phone;

            return View(nameof(Details), editUser);
        }
    }
}
