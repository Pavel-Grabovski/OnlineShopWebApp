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
        public async Task<IActionResult> DeleteAsync(string email)
        {
            var user = await usersManager.FindByEmailAsync(email);
            await usersManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetailsAsync(string email)
        {
            var user = await usersManager.FindByEmailAsync(email);
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

        public async Task<IActionResult> EditRightsAsync(string email)
        {
            var user = await usersManager.FindByEmailAsync(email);
            var userRoles = await usersManager.GetRolesAsync(user);
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
        public async Task<IActionResult> EditRightsAsync(string email, Dictionary<string, bool> userRolesViewModel)
        {
            var userSelectedRoles = userRolesViewModel.Select(x => x.Key);

            var user = await usersManager.FindByEmailAsync(email);
            var userRoles = await usersManager.GetRolesAsync(user);

            await usersManager.RemoveFromRolesAsync(user, userRoles);
            await usersManager.AddToRolesAsync(user, userSelectedRoles);

            return Redirect($"/Admin/User/Details?email={email}");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel changePassword)
        {
            if (changePassword.Email == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                var user = await usersManager.FindByEmailAsync(changePassword.Email);
                var newHashPassword = usersManager.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newHashPassword;
                await usersManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(changePassword);
        }

        public async Task<IActionResult> EditAsync(string email)
        {
            var user = await usersManager.FindByEmailAsync(email);
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
            return View("Details", userVM);
        }

    }
}
