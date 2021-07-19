using System.ComponentModel.DataAnnotations;
using Clean.Core.Resources;
using Clean.Core.Entities.Users;

namespace Clean.Core.Dto.Users.User
{
    public class CreateUserDto
    {
        [Display(ResourceType = typeof(DisplayNameResource), Name = "ExternalUserId")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(UserPropertiesConfiguration.ExternalUserIdMaxLength, ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "MaxLengthError")]
        public string ExternalUserId { get; set; }

        [Display(ResourceType = typeof(DisplayNameResource), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(UserPropertiesConfiguration.UserNameMaxLength, ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "MaxLengthError")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(DisplayNameResource), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(UserPropertiesConfiguration.EmailMaxLength, ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "MaxLengthError")]
        [EmailAddress(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "InvalidEmailError")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(DisplayNameResource), Name = "DisplayName")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(UserPropertiesConfiguration.DisplayNameMaxLength, ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "MaxLengthError")]
        public string DisplayName { get; set; }
    }
}
