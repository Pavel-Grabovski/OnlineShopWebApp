using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
    }
}
