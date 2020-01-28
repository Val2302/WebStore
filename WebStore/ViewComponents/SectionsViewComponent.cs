using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;

namespace WebStore.ViewComponents
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private IProductData _productData;

        public SectionsViewComponent(IProductData productData, IMapper mapper)
        {
            _mapper = mapper;
            _productData = productData;
        }

        public IViewComponentResult Invoke(string sectionId)
        {
            int.TryParse(sectionId, out var sectionIdInt);

            var sections = GetSections(sectionIdInt, out var parentSectionId);

            return View(new SectionCompleteViewModel
            {
                Sections = sections,
                CurrentSectionId = sectionIdInt,
                CurrentParentSectionId = parentSectionId
            });
        }

        private List<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var allSections = _productData.GetSections();

            var parentCategories = allSections.Where(p => !p.ParentId.HasValue).ToList();

            var parentSections = new List<SectionViewModel>();
            foreach (var parentCategory in parentCategories)
            {
                parentSections.Add(new SectionViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentSectionView = null
                });
            }
            foreach (var sectionViewModel in parentSections)
            {
                var childCategories = allSections.Where(c => c.ParentId.Equals(sectionViewModel.Id));
                foreach (var childCategory in childCategories)
                {
                    if (childCategory.Id == sectionId)
                        parentSectionId = sectionViewModel.Id;

                    sectionViewModel.ChildSections.Add(new SectionViewModel
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentSectionView = sectionViewModel
                    });
                }
                sectionViewModel.ChildSections = sectionViewModel.ChildSections.OrderBy(c =>
                c.Order).ToList();
            }
            parentSections = parentSections.OrderBy(c => c.Order).ToList();
            return parentSections;
        }

    }
}
