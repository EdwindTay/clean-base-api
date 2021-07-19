using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Core.Controller
{
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        /// <summary>
        /// Get User Account Id from http request (only available for requests with token)
        /// </summary>
        /// <returns></returns>
        protected long? GetCurrentUserId()
        {
            if (Request.HttpContext.Items["UserId"] != null)
            {
                return (long)Request.HttpContext.Items["UserId"];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get User External Id from identity (only available for requests with token)
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentUserExternalId()
        {
            var claims = User.Identity as ClaimsIdentity;
            return claims.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        /// <summary>
        /// Get User Email from identity (only available for requests with token)
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentUserEmail()
        {
            if (Request.HttpContext.Items["Email"] != null)
            {
                return (string)Request.HttpContext.Items["Email"];
            }
            else
            {
                var claims = User.Identity as ClaimsIdentity;
                return claims.FindFirst(ClaimTypes.Email).Value;
            }

        }
    }
}
