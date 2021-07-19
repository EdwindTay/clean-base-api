using System;
using System.Threading.Tasks;
using Clean.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Clean.Core.Exceptions;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Clean.Web.Middlewares
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, ExceptionHandlingConfiguration exceptionHandlingConfiguration)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, exceptionHandlingConfiguration.ShowStackTrace);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, bool showStackTrace)
        {
            _logger.LogError(ex, $"An unhandled exception has occurred.");

            //return error in json format
            string result;

            if (showStackTrace)
            {
                result = JsonSerializer.Serialize(new { message = ex.Message, stackTrace = ex.StackTrace });
            }
            else
            {
                result = JsonSerializer.Serialize(new { message = ex.Message });
            }

            context.Response.ContentType = "application/json";

            if (ex is FriendlyException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else if (ex is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            await context.Response.WriteAsync(result);
        }
    }
}
