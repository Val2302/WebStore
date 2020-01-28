using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Models.Account
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false,
             ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "RequiredErrorMessage"),
         EmailAddress(ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "EmailErrorMessage"),
         Display(Name = "DisplayEmail",
             ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string Email { get; set; }

        [MaxLength(256),
         Display(Name = "DisplayLogin",
             ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false,
             ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "RequiredErrorMessage"),
         DataType(DataType.Password),
         StringLength(maximumLength: 20,
             MinimumLength = 6,
             ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "StringLengthErrorMessage"),
         
         Display(Name = "DisplayPassword",
             ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string Password { get; set; }
        
        [DataType(DataType.Password),
         Compare(nameof(Password), 
             ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "IncorrectPassword"),
         Display(Name = "DisplayConfirmPassword",
             ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string ConfirmPassword { get; set; }
    }
}
