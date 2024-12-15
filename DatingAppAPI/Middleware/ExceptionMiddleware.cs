using API.Error;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                //set response headers with error information
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //create new response body holding error details, omit stack trace in non-dev environments
                var apiException = env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace) : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                //set to CamelCase and return the error in json format
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(apiException, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
