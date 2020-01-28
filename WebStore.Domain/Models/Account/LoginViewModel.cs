using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Models.Account
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false,
             ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "RequiredErrorMessage"),
         MaxLength(256),
         Display(Name = "DisplayEmailOrUserName",
             ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string EmailOrUserName { get; set; }

        [Required(AllowEmptyStrings = false,
             ErrorMessageResourceType = typeof(Resources.ResourcesModels.Resource),
             ErrorMessageResourceName = "RequiredErrorMessage"),
         DataType(DataType.Password),
         Display(Name = "DisplayPassword",
             ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public string Password { get; set; }

        [Display(Name = "DisplayRememberMe",
            ResourceType = typeof(Resources.ResourcesModels.Resource))]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
