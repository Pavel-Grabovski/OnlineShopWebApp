using OnlineShopWebApp.Models;

namespace OnlineShopWebApp
{
    public class UserManager:IUsersManager
    {
        private readonly ICollection<UserAccount> users = new List<UserAccount>();

        public void Add(UserAccount user)
        {
            users.Add(user);
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
