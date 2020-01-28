using System.Collections.Generic;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Brand : OrderNamedEntity
    {
        public virtual ICollection<Product> Products { get; set; }        
    }
}
