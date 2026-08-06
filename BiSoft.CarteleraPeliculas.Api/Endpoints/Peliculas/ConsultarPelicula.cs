using BiSoft.CarteleraPeliculas.Application.DTOs;
using BiSoft.CarteleraPeliculas.Application.Services;

namespace BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas
{
    public static class ConsultarPelicula
    {
        private const string ENDPOINT_NAME = "Consultar Pelicula por Id";

        public static RouteGroupBuilder MapConsultarPelicula(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, PeliculaService peliculaService) =>
            {
                var pelicula = await peliculaService.ConsultarPelicula(id);

                var response = new ConsultarPeliculaResponse
                {
                    Id = pelicula.Id,
                    Titulo = pelicula.Titulo,
                    ReleaseYear = pelicula.ReleaseYear,
                    Genero = pelicula.Genero,
                    PosterUrl = pelicula.PosterUrl,
                    ImdbRating = pelicula.ImdbRating,
                    Sinopsis = pelicula.Sinopsis,
                    Status = pelicula.Status
                };

                return Results.Ok(response);
            })
            .Produces<ConsultarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Consulta una pelicula por su identificador.")
            .WithSummary(ENDPOINT_NAME)
            .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}