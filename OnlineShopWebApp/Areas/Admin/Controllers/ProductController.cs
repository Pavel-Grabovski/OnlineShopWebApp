using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;
using OnlineShop.Db;
using OnlineShop.Entities;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area(Constants.AdminRoleName)]
[Authorize(Roles = Constants.AdminRoleName)]
public class ProductController : Controller
{
    private readonly IProductsRepository productRepository;
    private readonly ImagesProvider imagesProvider;
    private readonly IMapper mapper;

    public ProductController(IProductsRepository productRepository, ImagesProvider imagesProvider = null, IMapper mapper = null)
    {
        this.productRepository = productRepository;
        this.imagesProvider = imagesProvider;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var productsDb = await productRepository.GetAllAsync();
        var productsViewModels = mapper.Map<ICollection<ProductViewModel>>(productsDb);

        return View(productsViewModels);
    }

    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var product = await productRepository.TryGetByIdAsync(id);
        await productRepository.DeleteAsync(product);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditAsync(Guid id)
    {
        var product = await productRepository.TryGetByIdAsync(id);
        if (product != null)
        {
            var editProductVM = mapper.Map<EditProductViewModel>(product);
            return View(editProductVM);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(EditProductViewModel editProductVM)
    {
        if (ModelState.IsValid)
        {
            if (editProductVM.UploadedFiles != null && editProductVM.UploadedFiles.Length > 0)
            {

                var addedImagesPath = imagesProvider.SafeFiles(editProductVM.Name, editProductVM.UploadedFiles, ImageFolders.Products);
                if (editProductVM.ImagesPaths == null)
                {
                    editProductVM.ImagesPaths = new List<string>();
                }
                editProductVM.ImagesPaths.AddRange(addedImagesPath);
            }

            var product = mapper.Map<Product>(editProductVM);

            await productRepository.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(nameof(EditAsync), editProductVM);

    }
    public IActionResult AddAsync()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(AddProductViewModel AddProductVM)
    {
        if (ModelState.IsValid)
        {
            var imagesPaths = imagesProvider.SafeFiles(AddProductVM.Name, AddProductVM.UploadedFiles, ImageFolders.Products);

            var product = mapper.Map<Product>((AddProductVM, imagesPaths));

            await productRepository.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View("Add", AddProductVM);
    }
}
