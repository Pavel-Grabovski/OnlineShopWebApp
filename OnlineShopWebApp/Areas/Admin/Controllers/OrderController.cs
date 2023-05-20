using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(orders);
        }
        public IActionResult Details(Guid id)
        {
            var order = ordersRepository.TryGetByOrderId(id);
            return View(order);
        }

        public IActionResult SaveStatus(Guid id, OrderStatus status)
        {
            ordersRepository.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }
    }
}
