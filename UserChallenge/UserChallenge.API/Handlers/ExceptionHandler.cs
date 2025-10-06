using Microsoft.AspNetCore.Diagnostics;

namespace UserChallenge.API.Handlers
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex);
            }
        }

        private Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message = exception.Message;

            switch (exception)
            {
                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case ArgumentNullException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case InvalidOperationException:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            _logger.LogError(exception, "Ocurrió un error");

            context.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                message,
                detail = exception.InnerException?.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
