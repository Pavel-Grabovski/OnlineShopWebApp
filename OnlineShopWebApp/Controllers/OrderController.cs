using AutoMapper;
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
        private readonly IMapper mapper;
        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            if (cart != null)
            {
                ViewBag.Cart = mapper.Map<CartViewModel>(cart);
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
                    UserInfo = mapper.Map<UserDeliveryInfo>(userInfo),
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
