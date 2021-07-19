using Clean.DataAccess.Entities.Users;

namespace Clean.DataAccess.Entities.Base.Interfaces
{
    public interface IHasTenant
    {
        long? TenantId { get; set; }
        Tenant Tenant { get; set; }
    }
}
