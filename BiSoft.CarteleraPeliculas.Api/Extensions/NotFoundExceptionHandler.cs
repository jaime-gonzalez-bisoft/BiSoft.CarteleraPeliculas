using Microsoft.AspNetCore.Diagnostics;

namespace BiSoft.CarteleraPeliculas.Api.Extensions
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not KeyNotFoundException)
                return false;

            _logger.LogWarning(exception, "Recurso no encontrado.");

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                title = "No encontrado",
                status = StatusCodes.Status404NotFound,
                detail = exception.Message
            }, cancellationToken);

            return true;
        }
    }
}