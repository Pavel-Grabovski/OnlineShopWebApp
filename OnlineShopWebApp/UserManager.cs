using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class UserManager:IUsersManager
    {
        private readonly ICollection<UserAccount> users = new List<UserAccount>()
        {
            new UserAccount
            {
                Email = "Araa@mail.ru",
                Password = "password"
            }
        };

        public void Add(UserAccount user)
        {
            users.Add(user);
        }

        public void ChangePassword(string email, string password)
        {
            var account = TryGetByUserName(email);
            account.Password = password;
        }

        public void Delete(UserAccount user)
        {
            users.Remove(user);
        }

        public ICollection<UserAccount> GetAll()
        {
            return users;
        }

        public UserAccount TryGetByUserName(string email)
        {
            return users.FirstOrDefault(x => x.Email == email);
        }
    }
}
