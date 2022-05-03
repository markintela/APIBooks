using Core.Model;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace WebBooksAPI.Middleware
{
    public class ExceptionHadlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHadlingMiddleware(ILogger<ExceptionHadlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            string id = Activity.Current?.Id ?? context?.TraceIdentifier;
            var errorResponse = new ErrorResponse(id, exception.Message);
                      
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        _logger.LogError("Error::: {@ex}", ex.Message);
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    _logger.LogError("Error::: {@ex}", ex.Message);
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;            
                    _logger.LogError("Error::: {@ex}", ex.Message);
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server errors. Check Logs!";
                    break;
            }
  
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
