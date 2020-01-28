using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.Domain.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IProductData _productData;

        public SitemapController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index","Home")),
                new SitemapNode(Url.Action("Shop","Catalog")),
                new SitemapNode(Url.Action("BlogSingle","Home")),
                new SitemapNode(Url.Action("Blog","Home")),
                new SitemapNode(Url.Action("Contact","Home"))
            };

            var sections = _productData.GetSections();

            var parentCategories = sections.Where(p => !p.ParentId.HasValue).ToArray();

            foreach (var parentCategory in parentCategories)
            {
                var childCategories = sections.Where(c => c.ParentId.Equals(parentCategory.Id)).ToList();

                if (childCategories.Count == 0)
                    nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new {sectionId = parentCategory.Id})));

                foreach (var childCategory in childCategories)
                    nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new {sectionId = childCategory.Id})));
            }

            var brands = _productData.GetBrands();
            foreach (var brand in brands)
            {
                nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new
                {
                    brandId = brand.Id
                })));
            }

            var products = _productData.GetProducts(new ProductFilter()).Products;
            foreach (var productDto in products)
            {
                nodes.Add(new SitemapNode(Url.Action("ProductDetails",
                    "Catalog", new { id = productDto.Id })));
            }
            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }

    }
}