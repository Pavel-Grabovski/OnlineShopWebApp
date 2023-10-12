using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class AvatarViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;

        public AvatarViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var user = userManager.FindByEmailAsync(email).Result;
                if(user != null)
                {
                    var userVM = user.ToUserViewModel();
                    return View("Avatar" , userVM);
                }
            }
            return View();
        }
    }
}
