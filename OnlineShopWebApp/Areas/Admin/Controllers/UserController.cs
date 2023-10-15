using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> usersManager;
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly ImagesProvider imagesProvider;
        private readonly IMapper mapper;
        public UserController(UserManager<User> usersManager, RoleManager<IdentityRole> rolesManager, ImagesProvider imagesProvider, IMapper mapper)
        {
            this.usersManager = usersManager;
            this.rolesManager = rolesManager;
            this.imagesProvider = imagesProvider;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = usersManager.Users.ToList(); 
            return View(users.Select(user => mapper.Map<UserViewModel>(user)).ToList());
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
            var userVM = mapper.Map<UserViewModel>(user);
            return View(userVM);
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

        public IActionResult Edit(string email)
        {
            var user = usersManager.FindByEmailAsync(email).Result;
            if(user != null)
            {
                var editUser = mapper.Map<EditUserViewModel>(user);
                return View(editUser);
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public IActionResult Edit(EditUserViewModel editUserViewModel)
        {
            var user = usersManager.FindByEmailAsync(editUserViewModel.Email).Result;
            user.PhoneNumber = editUserViewModel.Phone;
            user.Name = editUserViewModel.Name;
            user.Surname = editUserViewModel.Surname;
            user.Patronymic = editUserViewModel.Patronymic;

            if(editUserViewModel.UploadFile != null)
            {
                var imagesPath = imagesProvider.SafeFiles(editUserViewModel.Email.Replace('@', '_'), editUserViewModel.UploadFile, ImageFolders.Profiles);
                user.ImagePath = imagesPath;
            }

            usersManager.UpdateAsync(user).Wait();

            var userVM = mapper.Map<UserViewModel>(editUserViewModel);
            return View(nameof(Details), userVM);
        }

    }
}
