using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Helpers;

namespace WebStore.Domain.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
            ErrorMessageResourceName = "RequiredErrorMessage"),
        StringLength(maximumLength: 200,
            MinimumLength = 2,
            ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        [Display(Name = "DisplayFirstName",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
            ErrorMessageResourceName = "RequiredErrorMessage"),
        Display(Name = "DisplaySecondName",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string SecondName { get; set; }

        [Display(Name = "DisplayPatronomyc",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string Patronymic { get; set; }

        [Display(Name = "DisplayGender",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public Gender Gender { get; set; }

        public string DisplayGenderEnumItem => Gender.GetEnumAttribute<DisplayAttribute>().Name;

        [Range(16,
            78,
            ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
            ErrorMessageResourceName = "RangeErrorMessage"),
        Display(Name = "DisplayAge",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public int Age { get; set; }

        [Display(Name = "DisplaySecretName",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string SecretName { get; set; }

        [Display(Name = "DisplayPosition",
            ResourceType = typeof(Resources.ResourcesModels.Resource)),
        Required(AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
            ErrorMessageResourceName = "RequiredErrorMessage")]
        public string Position { get; set; }
    }
}
