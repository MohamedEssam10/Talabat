using System.Text.Json;
using Talabat.API.Errors;

namespace Talabat.API.Middelwares
{
    // BY Convention
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //Take an action with the Request
                await next.Invoke(httpContext); //Go To the next Middleware
                //Take an action with the Response

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message); //Development
                // in Production Errors are logged into (database | Files)

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                var response = environment.IsDevelopment() ? new APIExceptionResponse(500, ex.Message, ex.StackTrace?.ToString()) : new APIExceptionResponse(500);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response,options);
                await httpContext.Response.WriteAsync(json);
            }

        }
    }
}
