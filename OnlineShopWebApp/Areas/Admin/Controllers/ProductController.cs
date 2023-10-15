using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using OnlineShopWebApp.Areas.Admin.Models;
using AutoMapper;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
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

        public IActionResult Index()
        {
            var productsDb = productRepository.GetAll();
            var productsViewModels = mapper.Map<ICollection<ProductViewModel>>(productsDb);

            return View(productsViewModels);
        }
        public IActionResult Delete(Guid id)
        {
            var product = productRepository.TryGetById(id);
            productRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var product = productRepository.TryGetById(id);
            if (product != null)
            {
                var editProductVM = mapper.Map<EditProductViewModel>(product);
                return View(editProductVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel editProductVM)
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

                productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Edit), editProductVM);

        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel AddProductVM)
        {
            if (ModelState.IsValid)
            {
                var imagesPaths = imagesProvider.SafeFiles(AddProductVM.Name, AddProductVM.UploadedFiles, ImageFolders.Products);

                var product = mapper.Map<Product>((AddProductVM, imagesPaths));

                productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Add), AddProductVM);
        }
    }
}
