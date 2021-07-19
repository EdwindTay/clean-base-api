using System.Security.Claims;
using System.Threading.Tasks;
using Clean.Application.Services.Users.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Clean.Web.Middlewares
{
    public static class UserInformationMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserInformation(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserInformationMiddleware>();
        }
    }

    public class UserInformationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInformationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var claims = context.User.Identity as ClaimsIdentity;
                var externalUserId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await userService.GetUserByExternalUserIdAsync(externalUserId: externalUserId);

                if (user != null)
                {
                    context.Items["UserId"] = user.UserId;
                    context.Items["Email"] = user.Email;
                    context.Items["TenantId"] = user.TenantId;
                }
            }

            await _next(context);
        }
    }
}
