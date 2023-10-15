using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
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

        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            var ordersVM = mapper.Map<ICollection<OrderViewModel>>(orders);
            return View(ordersVM);
        }
        public IActionResult Detail(Guid id)
        {
            var orderDb = ordersRepository.TryGetByOrderId(id);
            var orderVM = mapper.Map<OrderViewModel>(orderDb);
            return View(orderVM);
        }

        public IActionResult SaveStatus(Guid id, OrderStatusViewModel status)
        {
            ordersRepository.UpdateStatus(id, (OrderStatus)(int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}
