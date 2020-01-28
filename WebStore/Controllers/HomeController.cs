using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Filters;
using WebStore.Domain.Models;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Clients;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    /// <summary>
    /// The main controller. 
    /// The entry point to the site
    /// </summary>
    public class HomeController : Controller
    {
        //private const int IndexProductCount = 6;

        //private readonly IProductData _productData;

        //public HomeController(IProductData productData)
        //{
        //    _productData = productData;
        //}

        //public IActionResult Index()
        //{
        //    var model = from p in _productData.GetProducts(new ProductFilter()).Take(IndexProductCount)
        //                select new ProductViewModel
        //                {
        //                    Id = p.Id,
        //                    ImageUrl = p.ImageUrl,
        //                    Name = p.Name,
        //                    Order = p.Order,
        //                    Price = p.Price
        //                };

        //    return View(model);
        //}

        private readonly IValuesService _valuesService;

        public HomeController(IValuesService valueService)
        {
            _valuesService = valueService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _valuesService.GetAsync();

            return View(model);
        }

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("Page404");
            return Content($"Error status code: {id}");
        }

        public IActionResult Error() => View();

        public IActionResult Page404() => View();


        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Checkout() => View();
    }
}