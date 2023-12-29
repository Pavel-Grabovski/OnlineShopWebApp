using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IMapper mapper;
        public HomeController(IProductsRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var productsDb = await productRepository.GetAllAsync();
            var productsViewModels = mapper.Map<ICollection<ProductViewModel>>(productsDb);
            return View(productsViewModels);
        }
    }
}