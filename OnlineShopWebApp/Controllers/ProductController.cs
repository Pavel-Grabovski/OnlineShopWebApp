﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IMapper mapper;
        public ProductController(IProductsRepository productsRepository, IMapper mapper)
        {
            this.productRepository = productsRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var product = await productRepository.TryGetByIdAsync(id);
            if (product != null)
            {
                var model = mapper.Map<ProductViewModel>(product);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
