using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IOrdersRepository ordersRepository;

        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository)
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);

            ViewBag.Cart = cart;

            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserInfo userInfo)
        {
            if(ModelState.IsValid)
            {
                var existingCart = cartsRepository.TryGetByUserId(userInfo.UserId);
                var order = new Order
                {
                    UserInfo = userInfo,
                    Cart = existingCart,
                };

                ordersRepository.Add(order);
                cartsRepository.Clear(Constants.UserId);
                return View();
            }
            return View("Index", userInfo);
        }
    }
}
