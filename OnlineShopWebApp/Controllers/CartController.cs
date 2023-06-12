using Microsoft.AspNetCore.Mvc;
using OnlineShopDB;
using OnlineShopDB.Models;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly ICartsRepository cartsRepository;

        public CartController(IProductsRepository productRepository, ICartsRepository cartsRepository)
        {
            this.productRepository = productRepository;
            this.cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            return View(cart);
        }
        public IActionResult Add(Guid guid)
        {
            var productDB = productRepository.TryGetById(guid);



            //cartsRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult DecreaseAmount(Guid guid)
        {
            var product = productRepository.TryGetById(guid);
            //cartsRepository.DecreaseAmount(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(Guid guid)
        {
            var product = productRepository.TryGetById(guid);
            //cartsRepository.Remove(product, Constants.UserId);
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
