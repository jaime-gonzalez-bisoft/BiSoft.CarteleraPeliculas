using BiSoft.CarteleraPeliculas.Application.DTOs;
using BiSoft.CarteleraPeliculas.Application.Services;

namespace BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas
{
    public static class ConsultarPeliculas
    {
        private const string ENDPOINT_NAME = "Consultar Peliculas";

        public static RouteGroupBuilder MapConsultarPeliculas(this RouteGroupBuilder group)
        {
            group.MapGet("", (PeliculaService peliculaService) =>
            {
                var peliculas = peliculaService.ConsultarPeliculas();

                var response = peliculas.Select(pelicula => new ConsultarPeliculaResponse
                {
                    Id = pelicula.Id,
                    Titulo = pelicula.titulo,
                    ReleaseYear = pelicula.release_year,
                    Genero = pelicula.genero,
                    PosterUrl = pelicula.poster_url,
                    ImdbRating = pelicula.imdb_rating,
                    Sinopsis = pelicula.sinopsis,
                    Status = pelicula.IsDeleted ? 1 : 0
                });

                return Results.Ok(response);
            })
            .Produces<IEnumerable<ConsultarPeliculaResponse>>(StatusCodes.Status200OK)
            .WithDescription("Consulta todas las peliculas registradas en el sistema.")
            .WithSummary(ENDPOINT_NAME)
            .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}