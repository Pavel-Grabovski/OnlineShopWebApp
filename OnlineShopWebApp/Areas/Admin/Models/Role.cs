using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class Role
    {
        [Required(ErrorMessage = "Введите название роли")]
        public string Name { get; set; }
    }
}
