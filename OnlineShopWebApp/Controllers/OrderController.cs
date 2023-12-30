using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
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

        public async Task<IActionResult> Index()
        {
            var cart = await cartsRepository.TryGetByUserIdAsync(Constants.UserId);
            if (cart != null)
            {
                ViewBag.Cart = mapper.Map<CartViewModel>(cart);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyAsync(UserDeliveryInfoViewModel userInfo)
        {
            if(ModelState.IsValid)
            {
                var cartDb = await cartsRepository.TryGetByUserIdAsync(Constants.UserId);
                var order = new Order
                {
                    UserInfo = mapper.Map<UserDeliveryInfo>(userInfo),
                    Items = cartDb.Items
                };

                await ordersRepository.AddAsync(order);
                await cartsRepository.RemoveAsync(Constants.UserId);
                return View();
            }
            return View(nameof(Index), userInfo);
        }
    }
}
