using Unconnectedwebapi.Models;

namespace Unconnectedwebapi.CustomMiddleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
                
            }
            catch(Exception ex)
            {
                _logger.LogError("Something went wrong");
                await handleexception(context, ex);
            }
        }
        private static Task handleexception(HttpContext context, Exception ex)
        {
            CustomException cs = new()
            {
                statuscode = 500,
                message = ex.Message
            };
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(cs.ToString());
           


        }
    }
   public static class ExceptionMiddlewareExtensions
    {
        public static void configureExtension(this IApplicationBuilder app) { 
        
              app.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
