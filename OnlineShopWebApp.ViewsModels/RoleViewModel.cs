using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewsModels;

public class RoleViewModel
{
    [Required(ErrorMessage = "Введите название роли")]
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        var role = (RoleViewModel)obj;
        return Name == role.Name;
    }
}
