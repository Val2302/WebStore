using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    /// <summary>
    /// Entity section
    /// </summary>
    public class Section : OrderNamedEntity
    {
        /// <summary>
        /// Parent section (if available)
        /// </summary>
        public int? ParentId { get; set; }
    }
}
