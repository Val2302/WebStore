using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Dto.Product
{
    public class ProductDto : IOrderedEntity
    {
        public int Id { get; set; }

        [Display(Name = "DisplayName",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Name { get; set; }

        public int Order { get; set; }

        [Display(Name = "DisplayImageUrl",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string ImageUrl { get; set; }

        [Display(Name = "DisplayPrice",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public decimal Price { get; set; }

        [Display(Name = "DisplayBrand",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public BrandDto Brand { get; set; }

        [Display(Name = "DisplayCondition",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Condition { get; set; }

        [Display(Name = "DisplaySection",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public SectionDto Section { get; set; }

        [Display(Name = "DisplayQuantity",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public int Quantity { get; set; }
    }
}
