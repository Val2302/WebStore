
namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface IOrderedEntity : INamedEntity
    {
        int Order { get; set; }
    }
}
