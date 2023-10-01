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
        private readonly RoleManager<IdentityRole> rolesManager;

        public UserController(UserManager<User> usersManager, RoleManager<IdentityRole> rolesManager)
        {
            this.usersManager = usersManager;
            this.rolesManager = rolesManager;
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

        public IActionResult ChangePassword(string email)
        {
            var changePassword = new ChangePasswordViewModel()
            {
                Email = email
            };
            return View(changePassword);
        }

        public IActionResult EditRights(string email)
        {
            var user = usersManager.FindByEmailAsync(email).Result;
            var userRoles = usersManager.GetRolesAsync(user).Result;
            var roles = rolesManager.Roles.ToList();
            var model = new EditRightsViewModel
            {
                Email = user.Email,
                UserRoles = userRoles.Select(x => new RoleViewModel { Name = x }).ToList(),
                AllRoles = roles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
            };
            return View(model);
            
        }

        [HttpPost]
        public IActionResult EditRights(string email, Dictionary<string, bool> userRolesViewModel)
        {
            var userSelectedRoles = userRolesViewModel.Select(x => x.Key);

            var user = usersManager.FindByEmailAsync(email).Result;
            var userRoles = usersManager.GetRolesAsync(user).Result;

            usersManager.RemoveFromRolesAsync(user, userRoles).Wait();
            usersManager.AddToRolesAsync(user, userSelectedRoles).Wait();

            return Redirect($"/Admin/User/Details?email={email}");
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
