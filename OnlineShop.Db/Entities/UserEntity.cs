using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Entities;

public class UserEntity : IdentityUser
{
	public string? Name { get; set; } = string.Empty;
	public string? Patronymic { get; set; } = string.Empty;
	public string? Surname { get; set; } = string.Empty;
	public string? ImagePath { get; set; }
}
