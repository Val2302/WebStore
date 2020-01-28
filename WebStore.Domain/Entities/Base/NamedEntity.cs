using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        [Display(Name = "DisplayName",
            ResourceType = typeof(Resources.ResourcesEntities.Resource))]
        public string Name { get; set; }
    }
}
