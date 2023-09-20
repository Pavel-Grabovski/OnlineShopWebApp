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

        public OrderController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            var ordersVM = orders.Select(or => or.ToOrderViewModel()).ToList(); 
            return View(ordersVM);
        }
        public IActionResult Detail(Guid id)
        {
            var orderDb = ordersRepository.TryGetByOrderId(id);
            var orderVM = orderDb.ToOrderViewModel();
            return View(orderVM);
        }

        public IActionResult SaveStatus(Guid id, OrderStatusViewModel status)
        {
            ordersRepository.UpdateStatus(id, (OrderStatus)(int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}
