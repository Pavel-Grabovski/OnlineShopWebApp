using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using OnlineShop.Entities;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
	public class AvatarViewComponent : ViewComponent
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public AvatarViewComponent(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var user = userManager.FindByEmailAsync(email).Result;
                if(user != null)
                {
                    var userVM = mapper.Map<UserViewModel>(user);
                    return View("Avatar" , userVM);
                }
            }
            return View();
        }
    }
}
