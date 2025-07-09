using MentorAi_backd.Exceptions;
using MentorAi_backd.Models.Entity;
using Microsoft.AspNetCore.Builder;

namespace MentorAi_backd.MiddleWare
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddleWare> _logger;

        public Middleware(RequestDelegate next , ILogger<MiddleWare> logger)
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
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }
        //helper method togenerate a structured JSON response for exceptions.
        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var apiResponse = ApiResponse<object>.ErrorResponse("An unexpected error occurred.");

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    apiResponse = ApiResponse<object>.ErrorResponse(validationException.Message,validationException.Errors);
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
                    apiResponse = ApiResponse<object>.ErrorResponse(
                        context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
                             ? exception.Message : "An unexpected error occurred.Pleace try again later,");
                    break;
            }
            //Converts HttpStatusCode to int and sets it.
            //Serializes the apiResponse object to JSON and writes it to the response.
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
        }

        public static class MiddlewareExtention
        {
            public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
            {
                return app.UseMiddleware<Middleware>();
            }
        }
    }
    
}
