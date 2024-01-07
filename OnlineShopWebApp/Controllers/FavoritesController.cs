using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.BL;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.ViewsModels;
using Constants = OnlineShop.BL.Constants;

namespace OnlineShopWebApp.Controllers;

[Authorize]
public class FavoritesController : Controller
{
    private readonly IMapper mapper;
    private readonly IFavoriteServices favoriteServices;
    private readonly IProductsServices productsServices;

    public FavoritesController(IMapper mapper, IFavoriteServices favoriteServices, IProductsServices productsServices)
    {
        this.mapper = mapper;
        this.favoriteServices = favoriteServices;
        this.productsServices = productsServices;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Product> products = await favoriteServices.GetAllAsync(User.Identity.Name);
        var productsViewModels = mapper.Map<IEnumerable<ProductViewModel>>(products);
        return View(productsViewModels);
    }
    public async Task<IActionResult> AddAsync(Guid productId)
    {
        await favoriteServices.AddAsync(User.Identity.Name, productId);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> RemoveAsync(Guid productId)
    {
        await favoriteServices.RemoveAsync(User.Identity.Name, productId);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> ClearAsync()
    {
        await favoriteServices.ClearAsync(User.Identity.Name);
        return RedirectToAction(nameof(Index));
    }
}
