using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.BL.Interfaces;
using OnlineShopWebApp.ViewsModels;

namespace OnlineShopWebApp.Views.Shared.Components.Cart;

	public class AvatarViewComponent : ViewComponent
{
    private readonly IUsersServices usersServices;
    private readonly IMapper mapper;

    public AvatarViewComponent(IUsersServices usersServices, IMapper mapper)
    {
        this.usersServices = usersServices;
        this.mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (User.Identity.IsAuthenticated)
        {
            var email = User.Identity.Name;
            var user = await usersServices.FindByEmailAsync(email);
            if(user != null)
            {
                var userVM = mapper.Map<UserViewModel>(user);
                return View("Avatar" , userVM);
            }
        }
        return View();
    }
}
