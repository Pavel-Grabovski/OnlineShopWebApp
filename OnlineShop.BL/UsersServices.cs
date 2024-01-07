using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db.Entities;

namespace OnlineShop.BL;

public class UsersServices : IUsersServices
{
    private readonly UserManager<UserEntity> usersManager;
    private readonly IMapper mapper;
    private readonly SignInManager<UserEntity> signInManager;

    public static string AdminRoleName { get; } = Constants.AdminRoleName;
    public UsersServices(UserManager<UserEntity> usersManager, IMapper mapper, SignInManager<UserEntity> signInManager)
    {
        this.usersManager = usersManager;
        this.mapper = mapper;
        this.signInManager = signInManager;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        UserEntity[] usersEntities = await usersManager.Users.ToArrayAsync();
        return usersEntities.Select(userEntity =>mapper.Map<User>(userEntity));
    }
    public async Task<User> FindByEmailAsync(string email)
    {
        var userEntity = await usersManager.FindByEmailAsync(email);
        var user = mapper.Map<User>(userEntity);
        return user;
    }
    public async Task ChangePasswordAsync(ChangePassword сhangePassword)
    {
        var user = await usersManager.FindByEmailAsync(сhangePassword.Email);
        var newHashPassword = usersManager.PasswordHasher.HashPassword(user, сhangePassword.Password);
        user.PasswordHash = newHashPassword;
        await usersManager.UpdateAsync(user);
    }


    public async Task EditDataAsync(User editUser)
    {
        var userEntity = await usersManager.FindByEmailAsync(editUser.Email);
        userEntity.PhoneNumber = editUser.PhoneNumber;
        userEntity.Name = editUser.Name;
        userEntity.Surname = editUser.Surname;
        userEntity.Patronymic = editUser.Patronymic;
        userEntity.ImagePath = editUser.ImagePath;

        usersManager.UpdateAsync(userEntity).Wait();
    }
    public async Task<IEnumerable<string>> GetRolesAsync(string email)
    {
        var userEntity = await usersManager.FindByEmailAsync(email);
        IList<string> userRoles = await usersManager.GetRolesAsync(userEntity);
        return userRoles;
    }

    public async Task SetNewRolesAsync(string email, IEnumerable<string> roles)
    {
        var userEntity = await usersManager.FindByEmailAsync(email);
        IList<string> userRoles = await usersManager.GetRolesAsync(userEntity);

        await usersManager.RemoveFromRolesAsync(userEntity, userRoles);
        await usersManager.AddToRolesAsync(userEntity, roles);
    }

    public async Task DeleteAsync(string email)
    {
        var userEntity = await usersManager.FindByEmailAsync(email);
        await usersManager.DeleteAsync(userEntity);
    }

    public async Task<IdentityResult> CreateUser(User user, string password)
    {
        var userEntity = mapper.Map<UserEntity>(user);
        IdentityResult result = await usersManager.CreateAsync(userEntity, password);
        if (result.Succeeded)
        {
            await usersManager.AddToRoleAsync(userEntity, Constants.UserRoleName);
        }
        return result;
    }
    public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
    {
        SignInResult result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        return result;
    }

    public async Task SignInAsync(User user, bool isPersistent)
    {
        var userEntity = mapper.Map<UserEntity>(user);
        await signInManager.SignInAsync(userEntity, false);
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
