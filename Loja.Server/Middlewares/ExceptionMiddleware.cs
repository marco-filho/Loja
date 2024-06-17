using System.Text.Json;
using Loja.Domain.Exceptions;

namespace Loja.Server.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorMessage = new ErrorModel();

            var exceptionTypeName = exception.GetType().Name;

            switch (exceptionTypeName)
            {
                case NotFoundException.ExceptionType:
                    errorMessage.StatusCode = StatusCodes.Status404NotFound;
                    errorMessage.Message = exception.Message;
                    break;

                default:
                    errorMessage.StatusCode = StatusCodes.Status500InternalServerError;
                    errorMessage.Message = exception.Message;
                    break;
            }

            _logger.LogError(exception, $"An exception has occurred");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorMessage.StatusCode;

            return context.Response.WriteAsJsonAsync(errorMessage);
        }
    }


    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        private static readonly JsonSerializerOptions Options =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, Options);
        }
    }
}
