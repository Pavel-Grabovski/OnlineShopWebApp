using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using OnlineShopWebApp.ViewsModels;
using OnlineShopWebApp.Helpers;
using OnlineShop.BL.Interfaces;
using OnlineShop.BL.Domains;
using OnlineShop.BL;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area(Constants.AdminRoleName)]
[Authorize(Roles = Constants.AdminRoleName)]
public class UserController : Controller
{
    private readonly IUsersServices usersServices;
    private readonly IRolesServices rolesServices;
    private readonly ImagesProvider imagesProvider;
    private readonly IMapper mapper;

    public UserController(IUsersServices usersServices, IRolesServices rolesServices, ImagesProvider imagesProvider, IMapper mapper)
    {
        this.usersServices = usersServices;
        this.rolesServices = rolesServices;
        this.imagesProvider = imagesProvider;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var users = await usersServices.GetAllAsync();
        return View(users.Select(user => mapper.Map<UserViewModel>(user)).ToArray());
    }
    public async Task<IActionResult> DetailsAsync(string email)
    {
        var user = await usersServices.FindByEmailAsync(email);
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

    [HttpPost]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel changePasswordVM)
    {
        if (changePasswordVM.Email == changePasswordVM.Password)
        {
            ModelState.AddModelError("", "Логин и пароль не должны совпадать");
        }
        if (! ModelState.IsValid)
            return View(changePasswordVM);

        var changePassword = mapper.Map<ChangePassword>(changePasswordVM);
        await usersServices.ChangePasswordAsync(changePassword);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditDataAsync(string email)
    {
        var user = await usersServices.FindByEmailAsync(email);

        if (user == null)
            return RedirectToAction(nameof(Index));

        var editUser = mapper.Map<EditUserViewModel>(user);
        return View(editUser);
    }

    [HttpPost]
    public async Task<IActionResult> EditDataAsync(EditUserViewModel editUserViewModel)
    {
        if (editUserViewModel == null)
            return RedirectToAction(nameof(Index));

        if (editUserViewModel.UploadFile != null)
        {
            var imagesPath = imagesProvider.SafeFiles(editUserViewModel.Email.Replace('@', '_'), editUserViewModel.UploadFile, ImageFolders.Profiles);
            editUserViewModel.ImagePath = imagesPath;
        }

        var user = mapper.Map<User>(editUserViewModel);
        await usersServices.EditDataAsync(user);

        var userVM = mapper.Map<UserViewModel>(user);
        return View("Details", userVM);
    }


    public async Task<IActionResult> EditRolesAsync(string email)
    {
        var user = await usersServices.FindByEmailAsync(email);
        IEnumerable<string> userRoles = await usersServices.GetRolesAsync(email);
        IEnumerable<Role> roles = rolesServices.GetAllRoles();

        var model = new EditRolesViewModel
        {
            Email = user.Email,
            UserRoles = userRoles.Select(x => new RoleViewModel { Name = x }).ToList(),
            AllRoles = roles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditRolesAsync(string email, Dictionary<string, bool> userRolesViewModel)
    {
        IEnumerable<string> userSelectedRoles = userRolesViewModel.Select(x => x.Key).ToArray();

        await usersServices.SetNewRolesAsync(email, userSelectedRoles);

        return Redirect($"/Admin/User/Details?email={email}");
    }

    public async Task<IActionResult> DeleteAsync(string email)
    {
        await usersServices.DeleteAsync(email);
        return RedirectToAction(nameof(Index));
    }
}
