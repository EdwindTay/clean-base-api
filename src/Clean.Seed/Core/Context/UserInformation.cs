using Clean.DataAccess.Core.Context;
using static Clean.Core.Enums;

namespace Clean.Seed.Core.Context
{
    public class UserInformation : IUserContext
    {
        public long? GetUserId()
        {
            return (int)UserId.SystemUser;
        }

        public long? GetTenantId()
        {
            return null;
        }
    }
}
