using BiSoft.CarteleraPeliculas.Application.DTOs;
using BiSoft.CarteleraPeliculas.Application.Services;

namespace BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas
{
    public static class EliminarPelicula
    {
        private const string ENDPOINT_NAME = "Eliminar Pelicula";

        public static RouteGroupBuilder MapEliminarPelicula(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (Guid id, PeliculaService peliculaService) =>
            {
                var pelicula = await peliculaService.EliminarPelicula(id);

                return Results.Ok(pelicula);
            })
            .Produces<EliminarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Elimina una pelicula del sistema.")
            .WithSummary(ENDPOINT_NAME)
            .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}