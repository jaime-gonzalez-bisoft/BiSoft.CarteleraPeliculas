using BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas;
using Microsoft.OpenApi;

namespace BiSoft.CarteleraPeliculas.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app )
        {
            var apiEndpoints = app.MapGroup("api")
                                  .AddOpenApi();
            apiEndpoints.MapPeliculasEndpoints();
            return app;
        }
        private static RouteGroupBuilder AddOpenApi(this RouteGroupBuilder app)
        {
            return app.AddOpenApiOperationTransformer((options, context, ct) =>
            {
                options.Responses["400"] = new OpenApiResponse
                {
                    Description = "Solicitud incorrecta"
                };
                options.Responses["401"] = new OpenApiResponse
                {
                    Description = "No autorizado"
                };
                options.Responses["403"] = new OpenApiResponse
                {
                    Description = "Acceso no concedido"
                };
                options.Responses["404"] = new OpenApiResponse
                {
                    Description = "No encontrado"
                };
                options.Responses["429"] = new OpenApiResponse()
                {
                    Description = "Se ha excedido la cantidad de peticiones"
                };
                options.Responses["500"] = new OpenApiResponse
                {
                    Description = "Error interno del servidor"
                };
                return Task.CompletedTask;
            });
        }
    }
}
