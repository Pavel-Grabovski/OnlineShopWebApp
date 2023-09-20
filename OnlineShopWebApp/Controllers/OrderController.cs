using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
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
            if (cart != null)
            {
                ViewBag.Cart = cart.ToCartViewModel();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserDeliveryInfoViewModel userInfo)
        {
            if(ModelState.IsValid)
            {
                var cartDb = cartsRepository.TryGetByUserId(Constants.UserId);
                var order = new Order
                {
                    UserInfo = userInfo.ToUserDeliveryInfo(),
                    Items = cartDb.Items
                };

                ordersRepository.Add(order);
                cartsRepository.Remove(Constants.UserId);
                return View();
            }
            return View(nameof(Index), userInfo);
        }
    }
}
