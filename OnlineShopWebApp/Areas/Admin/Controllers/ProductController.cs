using Microsoft.AspNetCore.Mvc;
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
            var products = productRepository.GetAll();
            return View(products);
        }
        public IActionResult Delete(int id)
        {
            var product = productRepository.TryGetById(id);
            productRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var product = productRepository.TryGetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                var editProduct = productRepository.TryGetById(product.Id);

                if (editProduct != null)
                {
                    editProduct.Name = product.Name;
                    editProduct.Description = product.Description;
                    editProduct.Cost = product.Cost;

                    if (product.ImagePath != null)
                    {
                        editProduct.ImagePath = product.ImagePath;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Edit), product);

        }
        public IActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(NewProduct), product);
        }
    }
}
