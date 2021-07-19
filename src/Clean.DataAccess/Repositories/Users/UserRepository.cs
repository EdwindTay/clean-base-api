using Clean.DataAccess.Entities.Users;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.Repositories.Base;
using Clean.DataAccess.Repositories.Users.Interfaces;

namespace Clean.DataAccess.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CleanDbContext context) : base(context)
        {
        }
    }
}
