using System.ComponentModel.DataAnnotations;
using OpenDeploymentManager.Server.Contracts.Properties;

namespace OpenDeploymentManager.Server.Contracts
{
    public class ChangePassword
    {
        [Required(ErrorMessageResourceName = "RequiredAttribute_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Property_OldPassword", ResourceType = typeof(Resources))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredAttribute_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "StringLengthAttribute_ValidationErrorIncludingMinimum", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Property_NewPassword", ResourceType = typeof(Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceName = "ValidationError_ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Property_ConfirmPassword", ResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }

    public class SetPassword
    {
        [Required(ErrorMessageResourceName = "RequiredAttribute_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "StringLengthAttribute_ValidationErrorIncludingMinimum", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Display(Name = "Property_NewPassword", ResourceType = typeof(Resources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceName = "ValidationError_ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Property_ConfirmPassword", ResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }
}