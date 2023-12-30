using Microsoft.AspNetCore.Identity;

namespace OnlineShop.BL.Domains;

public class User : IdentityUser
{
	public string? Name { get; set; } = string.Empty;
	public string? Patronymic { get; set; } = string.Empty;
	public string? Surname { get; set; } = string.Empty;
	public string? ImagePath { get; set; }
}
