using Microsoft.Extensions.Logging;
using System.Net;

namespace FitnessSystem.Presentation.Middlewares
{
    public class ExceptionHandlerMiddlewares
    {
        private readonly ILogger<ExceptionHandlerMiddlewares> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddlewares(ILogger<ExceptionHandlerMiddlewares> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                _logger.LogError(ex, $"{errorId} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
