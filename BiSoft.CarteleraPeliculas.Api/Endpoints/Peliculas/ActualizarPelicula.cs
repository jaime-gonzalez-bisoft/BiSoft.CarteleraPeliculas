using BiSoft.CarteleraPeliculas.Api.DTOs.Pelicula;
using BiSoft.CarteleraPeliculas.Application.DTOs;
using BiSoft.CarteleraPeliculas.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas
{
    public static class ActualizarPelicula
    {
        private const string ENDPOINT_TITULO = "Actualizar Titulo";
        private const string ENDPOINT_RELEASE_YEAR = "Actualizar ReleaseYear";
        private const string ENDPOINT_GENERO = "Actualizar Genero";
        private const string ENDPOINT_POSTER = "Actualizar PosterUrl";
        private const string ENDPOINT_IMDB = "Actualizar ImdbRating";
        private const string ENDPOINT_SINOPSIS = "Actualizar Sinopsis";

        public static RouteGroupBuilder MapActualizarPelicula(this RouteGroupBuilder group)
        {
            group.MapPut("/{id:guid}/titulo", async (Guid id, [FromBody] UpdateTituloRequest request, PeliculaService service, CancellationToken ct) =>
            {
                var current = await service.ConsultarPelicula(id);
                var updated = await service.ActualizarPelicula(id, request.Titulo, current.ReleaseYear, current.Genero, current.PosterUrl, current.ImdbRating, current.Sinopsis);
                return Results.Ok(updated);
            })
            .Produces<ActualizarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ActualizarTitulo")
            .WithDescription("Actualiza el campo 'titulo' de una película.");

            group.MapPut("/{id:guid}/release_year", async (Guid id, [FromBody] UpdateReleaseYearRequest request, PeliculaService service, CancellationToken ct) =>
            {
                if (request.ReleaseYear < 1888 || request.ReleaseYear > 2100)
                    return Results.BadRequest("release_year fuera de rango.");

                var current = await service.ConsultarPelicula(id);
                var updated = await service.ActualizarPelicula(id, current.Titulo, request.ReleaseYear, current.Genero, current.PosterUrl, current.ImdbRating, current.Sinopsis);
                return Results.Ok(updated);
            })
            .Produces<ActualizarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ActualizarReleaseYear")
            .WithDescription("Actualiza el campo 'release_year' de una película.");

            group.MapPut("/{id:guid}/genero", async (Guid id, [FromBody] UpdateGeneroRequest request, PeliculaService service, CancellationToken ct) =>
            {
                var current = await service.ConsultarPelicula(id);
                var updated = await service.ActualizarPelicula(id, current.Titulo, current.ReleaseYear, request.Genero, current.PosterUrl, current.ImdbRating, current.Sinopsis);
                return Results.Ok(updated);
            })
            .Produces<ActualizarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ActualizarGenero")
            .WithDescription("Actualiza el campo 'genero' de una película.");

            group.MapPut("/{id:guid}/poster_url", async (Guid id, [FromBody] UpdatePosterUrlRequest request, PeliculaService service, CancellationToken ct) =>
            {
                var current = await service.ConsultarPelicula(id);
                var updated = await service.ActualizarPelicula(id, current.Titulo, current.ReleaseYear, current.Genero, request.PosterUrl, current.ImdbRating, current.Sinopsis);
                return Results.Ok(updated);
            })
            .Produces<ActualizarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ActualizarPosterUrl")
            .WithDescription("Actualiza el campo 'poster_url' de una película.");

            group.MapPut("/{id:guid}/imdb_rating", async (Guid id, [FromBody] UpdateImdbRatingRequest request, PeliculaService service, CancellationToken ct) =>
            {
                if (request.ImdbRating < 0f || request.ImdbRating > 10f)
                    return Results.BadRequest("imdb_rating debe estar entre 0 y 10.");

                var current = await service.ConsultarPelicula(id);
                var updated = await service.ActualizarPelicula(id, current.Titulo, current.ReleaseYear, current.Genero, current.PosterUrl, request.ImdbRating, current.Sinopsis);
                return Results.Ok(updated);
            })
            .Produces<ActualizarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ActualizarImdbRating")
            .WithDescription("Actualiza el campo 'imdb_rating' de una película.");

            group.MapPut("/{id:guid}/sinopsis", async (Guid id, [FromBody] UpdateSinopsisRequest request, PeliculaService service, CancellationToken ct) =>
            {
                var current = await service.ConsultarPelicula(id);
                var updated = await service.ActualizarPelicula(id, current.Titulo, current.ReleaseYear, current.Genero, current.PosterUrl, current.ImdbRating, request.Sinopsis);
                return Results.Ok(updated);
            })
            .Produces<ActualizarPeliculaResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("ActualizarSinopsis")
            .WithDescription("Actualiza el campo 'sinopsis' de una película.");

            return group;
        }
    }
}