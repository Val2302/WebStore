using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"),
    Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly IProductData _productData;
        private readonly IMapper _mapper;

        public HomeController(IProductData productData, IMapper mapper)
        {
            _productData = productData;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/products")]
        public IActionResult ProductList()
        {
            var products = _productData.GetProducts(new ProductFilter());
            return View(products);
        }

        public IActionResult Edit(int? id)
        {
            var notParentSections = _productData.GetSections()
                .Where(s => s.ParentId != null);

            var brands = _productData.GetBrands();

            if (!id.HasValue)
            {
                return View(new ProductViewModel()
                {
                    Sections = new SelectList(notParentSections, "Id", "Name"),
                    Brands = new SelectList(brands, "Id", "Name")
                });
            }

            var product = _productData.GetProductById(id.Value);

            if (product == null)
                return NotFound();

            var model = _mapper.Map<ProductViewModel>(product);

            model.Brands = new SelectList(brands, 
                "Id",
                "Name",
                product.Brand?.Id);

            model.Sections = new SelectList(notParentSections,
                "Id", 
                "Name",
                product.Section.Id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            var notParentSections = _productData.GetSections().Where(s =>
                s.ParentId != null);

            var brands = _productData.GetBrands();

            var section = _productData.GetSectionById(model.SectionId);

            BrandDto brand = null;
            if (model.BrandId.HasValue)
                brand = _productData.GetBrandById(model.BrandId.Value);

            if (ModelState.IsValid)
            {
                var productDto = new ProductDto()
                {
                    Id = model.Id,
                    ImageUrl = model.ImageUrl,

                    Name = model.Name,
                    Order = model.Order,
                    Price = model.Price,
                    Brand = brand,
                    Section = section
                };
                if (model.Id > 0)
                {
                    _productData.UpdateProduct(productDto);
                }
                else
                {
                    _productData.CreateProduct(productDto);
                }
                return RedirectToAction(nameof(ProductList));
            }
            model.Brands = new SelectList(brands, "Id", "Name", model.BrandId);
            model.Sections = new SelectList(notParentSections, "Id", "Name",
                model.SectionId);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _productData.DeleteProduct(id);
            return RedirectToAction("ProductList");
        }
    }
}