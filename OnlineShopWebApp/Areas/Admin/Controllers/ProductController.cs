using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Helpers;

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
            var productsDb = productRepository.GetAll();
            var productsViewModels = productsDb.ToProductsViewModels();
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
            var productDb = productRepository.TryGetById(id);
            if (productDb != null)
            {
                var productVM = productDb.ToProductViewModel();
                return View(productVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var editProductDb = productRepository.TryGetById(productVM.Id);

                if (editProductDb != null)
                {
                    editProductDb.Name = productVM.Name;
                    editProductDb.Description = productVM.Description;
                    editProductDb.Cost = productVM.Cost;

                    if (productVM.ImagePath != null)
                    {
                        editProductDb.ImagePath = productVM.ImagePath;
                    }
                    productRepository.Update(editProductDb);
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
                var productDb = productVM.ToProductDb();
                productRepository.Add(productDb);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Add), productVM);
        }
    }
}
