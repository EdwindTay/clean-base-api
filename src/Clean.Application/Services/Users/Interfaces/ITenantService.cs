using System.Threading.Tasks;
using Clean.Core.Dto.Users.Tenant;

namespace Clean.Application.Services.Users.Interfaces
{
    public interface ITenantService
    {
        /// <summary>
        /// Get tenant by id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<TenantDto> GetTenantAsync(long tenantId);

        /// <summary>
        /// Create tenant
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateTenantAsync(CreateTenantDto input);
    }
}
