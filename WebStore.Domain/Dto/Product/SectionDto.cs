using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Dto.Product
{
    public class SectionDto : IOrderedEntity
    {
        public int? ParentId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
