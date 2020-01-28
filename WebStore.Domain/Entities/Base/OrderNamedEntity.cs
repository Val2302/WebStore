using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    public abstract class OrderNamedEntity : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
