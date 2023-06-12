using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopDB.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsRepository productRepository;

        public ProductController(IProductsRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var productsDB = productRepository.GetAll();

            var productsViewModels = new List<ProductViewModel>();
            foreach (var productDB in productsDB)
            {
                var productVM = new ProductViewModel
                {
                    Id = productDB.Id,
                    Name = productDB.Name,
                    Cost = productDB.Cost,
                    Description = productDB.Description,
                    ImagePath = productDB.ImagePath
                };
                productsViewModels.Add(productVM);
            }

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
                var productVM = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                };
                return View(productVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Save(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var editProduct = productRepository.TryGetById(productVM.Id);

                if (editProduct != null)
                {
                    editProduct.Name = productVM.Name;
                    editProduct.Description = productVM.Description;
                    editProduct.Cost = productVM.Cost;
                    editProduct.ImagePath = productVM.ImagePath;
                    productRepository.Edit(editProduct);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Edit), productVM);

        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var productDB = new Product
                {
                    Name = productVM.Name,
                    Cost = productVM.Cost,
                    Description = productVM.Description,
                    ImagePath = productVM.ImagePath,
                };
                productRepository.Add(productDB);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Add), productVM);
        }
    }
}
