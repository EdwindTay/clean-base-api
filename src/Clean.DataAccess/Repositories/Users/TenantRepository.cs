using Clean.DataAccess.Entities.Users;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.Repositories.Base;
using Clean.DataAccess.Repositories.Users.Interfaces;

namespace Clean.DataAccess.Repositories.Users
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(CleanDbContext context) : base(context)
        {
        }
    }
}
