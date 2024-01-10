using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using System.Net;
using System.Text.Json;

namespace ProductManagementService.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentNullException argumentNullException)
            {
                _logger.LogError(argumentNullException, argumentNullException.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, argumentNullException);
            }
            catch (InvalidSubCategoryIdentifierException invalidSubCategoryIdentifierException)
            {
                _logger.LogError(invalidSubCategoryIdentifierException, invalidSubCategoryIdentifierException.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, invalidSubCategoryIdentifierException);
            }
            catch (InvalidCategoryIdentifierException invalidCategoryIdException)
            {
                _logger.LogError(invalidCategoryIdException, invalidCategoryIdException.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, invalidCategoryIdException);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpStatusCode, Exception exception)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            ProblemDetails problemDetails = new()
            {
                Status = (int)httpStatusCode,
                Title = exception.Message,
                Detail = exception.InnerException?.Message
            };

            context.Response.ContentType = "application/json";
            string json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }
    }

    public static class LogURLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
