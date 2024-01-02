using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using OnlineShop.BL.Interfaces;
using OnlineShopWebApp.ViewsModels;

namespace OnlineShopWebApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductsServicies productsServicies;
    private readonly IMapper mapper;

    public ProductController(IProductsServicies productsServicies, IMapper mapper)
    {
        this.productsServicies = productsServicies;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index(Guid id)
    {
        var product = await productsServicies.TryGetByIdAsync(id);
        if (product == null)
            return RedirectToAction("Index", "Home");

        var model = mapper.Map<ProductViewModel>(product);
        return View(model);
    }
}
