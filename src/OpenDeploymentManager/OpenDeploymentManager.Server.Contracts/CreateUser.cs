using System.ComponentModel.DataAnnotations;
using OpenDeploymentManager.Server.Contracts.Properties;

namespace OpenDeploymentManager.Server.Contracts
{
    public class CreateUser : User
    {
        [Required(ErrorMessageResourceName = "RequiredAttribute_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Property_UserName", ResourceType = typeof(Resources))]
        public override string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredAttribute_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "StringLengthAttribute_ValidationErrorIncludingMinimum", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Property_Password", ResourceType = typeof(Resources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "ValidationError_ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Property_ConfirmPassword", ResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }
}