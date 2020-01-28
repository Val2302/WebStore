using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using WebStore.Domain.Filters;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using WebStore.Domain.Models;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductData _productData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration, IMapper mapper)
        {
            _productData = productData;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            var pageSize = int.Parse(_configuration["PageSize"]);

            var pagedProducts = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = pageSize
            });

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = _mapper.Map<IEnumerable<ProductViewModel>>(pagedProducts.Products)
                    .ToList(),
                PageViewModel = new PageViewModel
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    TotalItems = pagedProducts.TotalCount
                }
            };

            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);

            if (product == null)
                return NotFound();

            var model = _mapper.Map<ProductViewModel>(product);

            return View(model);
        }

        public IActionResult GetFilteredItems(int? sectionId, int? brandId, int
            page = 1)
        {
            var productsModel = GetProducts(sectionId, 
                brandId,
                page, 
                out var totalCount);

            return PartialView("Partial/_Features", productsModel);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? sectionId, int?
            brandId, int page, out int totalCount)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = int.Parse(_configuration["PageSize"])
            });
            totalCount = products.TotalCount;

            return _mapper.Map<List<ProductViewModel>>(products.Products);
        }
    }
}