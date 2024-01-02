using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using OnlineShop.BL.Interfaces;
using OnlineShop.BL.Domains;
using OnlineShopWebApp.ViewsModels;

namespace OnlineShopWebApp.Controllers;

public class HomeController : Controller
{
    private readonly IProductsServicies productsServicies;
    private readonly IMapper mapper;
    public HomeController(IProductsServicies productsServicies, IMapper mapper)
    {
        this.productsServicies = productsServicies;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
		IEnumerable<Product> products = await productsServicies.GetAllAsync();
		IEnumerable<ProductViewModel> productsViewModels = mapper.Map<IEnumerable<ProductViewModel>>(products);
        return View(productsViewModels);
    }
}