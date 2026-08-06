using BiSoft.CarteleraPeliculas.Api.DTOs.Pelicula;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas
{
    public static class RegistrarPelicula
    {
        private const string ENDPOINT_NAME = "Registrar Pelicula";
        public static RouteGroupBuilder MapRegistrarPelicula(this RouteGroupBuilder group)
        {
            group.MapPost("", async ([FromBody] RegistrarPeliculaRequest request, PeliculaService peliculaService, CancellationToken ct) =>
            {
                var pelicula = await peliculaService.RegistrarPelicula(request.Titulo, request.Release_year, request.Genero, request.Poster_url, request.Imdb_rating, request.Sinopsis);
                var response = new RegistrarPeliculaResponse
                {
                    ProjectId = pelicula.ProjectId,
                    Titulo = pelicula.Titulo,
                    Release_year = pelicula.Release_year,
                    Genero = pelicula.Genero,
                    Poster_url = pelicula.Poster_url,
                    Imdb_rating = pelicula.Imdb_rating,
                    Sinopsis = pelicula.Sinopsis
                };
                return Results.Created($"/api/projects/{pelicula.ProjectId}", response);
            })
            .Produces<RegistrarPeliculaResponse>(StatusCodes.Status201Created)
            .WithDescription("Registra una nueva pelicula en el sistema.")
            .WithSummary(ENDPOINT_NAME)
            .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
