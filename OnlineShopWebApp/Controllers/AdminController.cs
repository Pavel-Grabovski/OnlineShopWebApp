using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductsRepository productRepository;

        public AdminController(IProductsRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Products()
        {
            var products = productRepository.GetAll();
            return View(products);
        }
        public IActionResult DeleteProduct(int productId)
        {
            var product = productRepository.TryGetById(productId);
            productRepository.Delete(product);
            return RedirectToAction("Products");
        }
        public IActionResult ProductEditor(int productId)
        {
            var product = productRepository.TryGetById(productId);
                return View(product);
        }

        [HttpPost]
        public IActionResult SaveProduct(Product product)
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
                return RedirectToAction("Products");
            }
            return View("ProductEditor", product);
           
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
                return RedirectToAction("Products");
            }
            return View("NewProduct", product);
        }

        public IActionResult Roles()
        {
            return View();
        }
    }
}
