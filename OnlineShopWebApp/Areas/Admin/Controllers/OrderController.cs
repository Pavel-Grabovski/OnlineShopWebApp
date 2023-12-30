using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
	[Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IMapper mapper;

        public OrderController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await ordersRepository.GetAllAsync();
            var ordersVM = mapper.Map<ICollection<OrderViewModel>>(orders);
            return View(ordersVM);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var orderDb = await ordersRepository.TryGetByOrderIdAsync(id);
            var orderVM = mapper.Map<OrderViewModel>(orderDb);
            return View(orderVM);
        }

        public async Task<IActionResult> SaveStatusAsync(Guid id, OrderStatusViewModel status)
        {
            await ordersRepository.UpdateStatusAsync(id, (OrderStatus)(int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}
