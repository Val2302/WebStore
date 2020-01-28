using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public bool IsMan { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string SecretName { get; set; }
        public string Position { get; set; }
    }
}
