using MentorAi_backd.Application.Common.Exceptions;

using System.Net;
using System.Text.Json;


// Corrected namespace to PascalCase
namespace MentorAi_backd.Middleware
{
    // Renamed the class for better clarity
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger; // Use the correct class name for logger

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing.");
                await HandleExceptionAsync(context, ex);
            }
        }

        // Helper method to generate a structured JSON response for exceptions.
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError; // Default to 500 Internal Server Error
            var apiResponse = ApiResponse<object>.ErrorResponse("An unexpected error occurred.");

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest; // 400 Bad Request
                    apiResponse = ApiResponse<object>.ErrorResponse(validationException.Message, validationException.Errors);
                    break;
                case NotFoundException _: 
                    statusCode = HttpStatusCode.NotFound; // 404 Not Found
                    apiResponse = ApiResponse<object>.ErrorResponse(exception.Message);
                    break;
                case UnauthorizedException _:
                    statusCode = HttpStatusCode.Unauthorized; // 401 Unauthorized
                    apiResponse = ApiResponse<object>.ErrorResponse(exception.Message);
                    break;
                case ForbiddenException _:
                    statusCode = HttpStatusCode.Forbidden; // 403 Forbidden
                    apiResponse = ApiResponse<object>.ErrorResponse(exception.Message);
                    break;
                case ConflictException _:
                    statusCode = HttpStatusCode.Conflict; // 409 Conflict
                    apiResponse = ApiResponse<object>.ErrorResponse(exception.Message);
                    break;
                case BadRequestException _:
                    statusCode = HttpStatusCode.BadRequest; // 400 Bad Request
                    apiResponse = ApiResponse<object>.ErrorResponse(exception.Message);
                    break;
               
                default:
                    // For unhandled exceptions, hide sensitive details in production
                    // Access IWebHostEnvironment to check if it's Development or Production
                    var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                    apiResponse = ApiResponse<object>.ErrorResponse(
                        environment.IsDevelopment()
                            ? exception.Message 
                            : "An unexpected error occurred. Please try again later."); 
                    break;
            }

           
            context.Response.StatusCode = (int)statusCode;
            // Serializes the apiResponse object to JSON and writes it to the response.
            await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
        }
    }

    // Corrected extension class name
    public static class GlobalExceptionHandlingMiddlewareExtensions
    {
        
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
         
            return app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
