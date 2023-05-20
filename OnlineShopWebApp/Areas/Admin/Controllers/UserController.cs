using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IRolesRepository rolesRepository;

        public UserController(IProductsRepository productRepository, IOrdersRepository ordersRepository, IRolesRepository rolesRepository)
        {
            this.productRepository = productRepository;
            this.ordersRepository = ordersRepository;
            this.rolesRepository = rolesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Orders
        public IActionResult Orders()
        {
            var orders = ordersRepository.GetAll();
            return View(orders);
        }
        public IActionResult OrderDetails(Guid id)
        {
            var order = ordersRepository.TryGetByOrderId(id);
            return View(order);
        }

        public IActionResult SaveOrderStatus(Guid id, OrderStatus status)
        {
            ordersRepository.UpdateStatus(id, status);
            return RedirectToAction("Orders");
        }

        #endregion


        public IActionResult Users()
        {
            return View();
        }

        #region Products
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
        #endregion

        #region Roles

        public IActionResult Roles()
        {
            var roles = rolesRepository.GetAll();
            return View(roles);
        }
        public IActionResult DeleteRole(string name)
        {
            rolesRepository.Delete(name);
            return RedirectToAction("Roles");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            if (rolesRepository.TryGetByName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует");
            }

            if (ModelState.IsValid)
            {
                rolesRepository.Add(role);
                return RedirectToAction("Roles");
            }
            return View(role);
        }
        #endregion
    }
}
