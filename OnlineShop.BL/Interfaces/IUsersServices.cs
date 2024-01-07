using Microsoft.AspNetCore.Identity;
using OnlineShop.BL.Domains;

namespace OnlineShop.BL.Interfaces;

public interface IUsersServices
{
    static string AdminRoleName;
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> FindByEmailAsync(string email);
    Task DeleteAsync(string email);
    Task EditDataAsync(User user);
    Task ChangePasswordAsync (ChangePassword сhangePassword);
    Task<IEnumerable<string>> GetRolesAsync(string email);
    Task SetNewRolesAsync(string email, IEnumerable<string> roles);
    Task<IdentityResult> CreateUser(User user, string password);
    Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);
    Task SignInAsync(User user, bool isPersistent);
    Task SignOutAsync();
}
