using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Models
{
    public enum Gender
    {
        [Display(Name = "мужской")]
        Man,
        [Display(Name = "женский")]
        Woman
    }
}
