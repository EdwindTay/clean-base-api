using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Services.Users.Interfaces;
using Clean.Core.Dto.Users.Tenant;
using Clean.Core.Exceptions;
using Clean.Core.Resources;
using Clean.DataAccess.Entities.Users;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.Repositories.Users.Interfaces;
using Clean.DataAccess.UnitOfWork.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clean.Application.Services.Users
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork<CleanDbContext> _unitOfWork;
        private readonly ITenantRepository _tenantRepository;

        public TenantService(
            IUnitOfWork<CleanDbContext> unitOfWork,
            ITenantRepository tenantRepository)
        {
            _unitOfWork = unitOfWork;
            _tenantRepository = tenantRepository;
        }

        ///<inheritdoc/>
        public async Task<TenantDto> GetTenantAsync(long tenantId)
        {
            var tenant = await _tenantRepository.Get()
                .AsNoTracking()
                .Select(a => new TenantDto
                {
                    TenantId = a.TenantId,
                    Name = a.Name
                })
                .FirstOrDefaultAsync(a => a.TenantId == tenantId);

            return tenant;
        }

        ///<inheritdoc/>
        public async Task CreateTenantAsync(CreateTenantDto input)
        {
            #region validation

            var isTenantExist = await _tenantRepository.Get()
                .AnyAsync(a => a.Name == input.Name);

            if (isTenantExist)
            {
                throw new FriendlyException(ErrorMessageResource.TenantAlreadyExistsError);
            }

            #endregion

            var tenant = new Tenant
            {
                Name = input.Name
            };

            _tenantRepository.Add(tenant);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
