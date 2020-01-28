using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebStore.Domain.Models.Product
{
    public class ProductViewModel : IOrderedEntity
    {
        public int Id { get; set; }

        [Required,
         Display(Name = "DisplayName",
             ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Name { get; set; }


        public int Order { get; set; }

        [Required,
         Display(Name = "DisplayImageUrl",
             ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string ImageUrl { get; set; }

        [Display(Name = "DisplayBrand",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Brand { get; set; }

        public int? BrandId { get; set; }

        [Display(Name = "DisplaySection",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Section { get; set; }

        [Required,
         Display(Name = "DisplayPrice",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public decimal Price { get; set; }

        [Required]
        public int SectionId { get; set; }
        public SelectList Sections { get; set; }
        public SelectList Brands { get; set; }


        public string Condition { get; set; }
        public int Quantity { get; set; }
    }
}