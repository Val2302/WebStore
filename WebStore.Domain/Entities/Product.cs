using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product : OrderNamedEntity
    {
        /// <summary>
        /// Section Id of product
        /// </summary>        
        [Display(Name = "DisplaySection",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public virtual Section Section { get; set; }

        /// <summary>
        /// Brand Id of product
        /// </summary>
        [Display(Name = "DisplayBrand",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual Brand Brand { get; set; }
        
        /// <summary>
        /// Product image link
        /// </summary>
        [Display(Name = "DisplayImageUrl",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string ImageUrl { get; set; }


        /// <summary>
        /// Product price
        /// </summary>
        [Display(Name = "DisplayPrice",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity in stock
        /// </summary>
        [Display(Name = "DisplayQuantity",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public int Quantity { get; set; }

        /// <summary>
        /// Product state
        /// </summary>
        [Display(Name = "DisplayCondition",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Condition { get; set; }

        /// <summary>
        /// Delete or not product 
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
