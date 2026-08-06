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
            if (exception is KeyNotFoundException)
            {
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

            if (exception is ArgumentException)
            {
                _logger.LogInformation(exception, "Solicitud inválida.");
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    title = "Solicitud inválida",
                    status = StatusCodes.Status400BadRequest,
                    detail = exception.Message
                }, cancellationToken);
                return true;
            }

            // Default: no handle here — que la cadena de middleware devuelva 500 o delegue a otro handler
            _logger.LogError(exception, "Error no controlado.");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                title = "Error interno del servidor",
                status = StatusCodes.Status500InternalServerError,
                detail = "Ocurrió un error inesperado."
            }, cancellationToken);

            return true;
        }
    }
}