using System.ComponentModel.DataAnnotations;
using Clean.Core.Resources;
using Clean.Core.Entities.Users;

namespace Clean.Core.Dto.Users.Tenant
{
    public class CreateTenantDto
    {
        [Display(ResourceType = typeof(DisplayNameResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "RequiredError")]
        [MaxLength(TenantPropertiesConfiguration.NameMaxLength, ErrorMessageResourceType = typeof(ErrorMessageResource), ErrorMessageResourceName = "MaxLengthError")]
        public string Name { get; set; }
    }
}
