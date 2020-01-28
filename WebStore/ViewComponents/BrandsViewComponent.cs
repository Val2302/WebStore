using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData, IMapper mapper)
        {
            _mapper = mapper;
            _productData = productData;
        }

        public IViewComponentResult Invoke(string brandId)
        {
            Int32.TryParse(brandId, out var brandIdInt);

            var brands = GetBrands();

            return View(new BrandCompleteViewModel
            {
                Brands = brands,
                CurrentBrandId = brandIdInt
            });
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _mapper.Map<IEnumerable<BrandViewModel>>(
                _productData.GetBrands()).ToList();

            for (int i = 0; i < brands.Count; i++)
                brands[i].ProductsCount = _productData.GetBrandProductCount(brands[i].Id);

            return brands;
        }

    }
}
