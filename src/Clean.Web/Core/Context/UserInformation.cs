using Clean.DataAccess.Core.Context;
using Microsoft.AspNetCore.Http;

namespace Clean.Web.Core.Context
{
    public class UserInformation : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInformation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? GetUserId()
        {
            //get user id from http context if available (passed from UserInformationMiddleware)
            long? userId = null;

            if (_httpContextAccessor?.HttpContext.Items["UserId"] != null)
            {
                userId = (long)_httpContextAccessor?.HttpContext.Items["UserId"];
            }

            return userId;
        }

        public long? GetTenantId()
        {
            //get tenant id from http context if available (passed from UserInformationMiddleware)
            long? tenantId = null;

            if (_httpContextAccessor?.HttpContext.Items["TenantId"] != null)
            {
                tenantId = (long)_httpContextAccessor?.HttpContext.Items["TenantId"];
            }

            return tenantId;
        }
    }
}
