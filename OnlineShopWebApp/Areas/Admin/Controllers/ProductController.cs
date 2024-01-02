using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using OnlineShop.BL.Interfaces;
using OnlineShop.BL.Domains;
using OnlineShop.Db;
using OnlineShopWebApp.ViewsModels;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area(Constants.AdminRoleName)]
[Authorize(Roles = Constants.AdminRoleName)]
public class ProductController : Controller
{
    private readonly IProductsServicies productsServicies;
    private readonly ImagesProvider imagesProvider;
    private readonly IMapper mapper;

    public ProductController(IProductsServicies productsServicies, ImagesProvider imagesProvider = null, IMapper mapper = null)
    {
        this.productsServicies = productsServicies;
        this.imagesProvider = imagesProvider;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Product> products = await productsServicies.GetAllAsync();
        IEnumerable<ProductViewModel> productsViewModels = mapper.Map<IEnumerable<ProductViewModel>>(products);

        return View(productsViewModels);
    }

    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await productsServicies.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditAsync(Guid id)
    {
        var product = await productsServicies.TryGetByIdAsync(id);
        if (product == null)
            return RedirectToAction(nameof(Index));

        var editProductVM = mapper.Map<EditProductViewModel>(product);
        return View(editProductVM);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(EditProductViewModel editProductVM)
    {
        if (!ModelState.IsValid)
            return View(nameof(EditAsync), editProductVM);

        IEnumerable<string> addedImagesPath = imagesProvider.SafeFiles(editProductVM.Name, editProductVM.UploadedFiles, ImageFolders.Products);

        if (editProductVM.ImagesPaths == null)
        {
            editProductVM.ImagesPaths = new List<string>();
        }
        editProductVM.ImagesPaths.AddRange(addedImagesPath);

        var product = mapper.Map<Product>(editProductVM);

        await productsServicies.UpdateAsync(product);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult AddAsync()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddProductViewModel AddProductVM)
    {
        if (!ModelState.IsValid)
            return View("Add", AddProductVM);

        IEnumerable<string> imagesPaths = imagesProvider.SafeFiles(AddProductVM.Name, AddProductVM.UploadedFiles, ImageFolders.Products);

        var product = mapper.Map<Product>((AddProductVM, imagesPaths));

        await productsServicies.AddAsync(product);
        return RedirectToAction(nameof(Index));
    }
}
