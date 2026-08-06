using BiSoft.CarteleraPeliculas.Application.DTOs;
using BiSoft.CarteleraPeliculas.Domain.Entities;
using BiSoft.CarteleraPeliculas.Domain.Services;
using Mapster;
using Microsoft.Extensions.Logging;

namespace BiSoft.CarteleraPeliculas.Application.Services
{
    public class PeliculaService
    {
        private readonly ILogger<PeliculaService> _logger;
        private readonly PeliculaDomainService _peliculaDomainService;

        public PeliculaService(
            ILogger<PeliculaService> logger,
            PeliculaDomainService peliculaDomainService)
        {
            _logger = logger;
            _peliculaDomainService = peliculaDomainService;
        }

        public async Task<RegistrarPeliculaResponse> RegistrarPelicula(
            string titulo,
            int release_year,
            string genero,
            string poster_url,
            float imdb_rating,
            string sinopsis)
        {
            var pelicula = await _peliculaDomainService.RegistrarPelicula(
                titulo,
                release_year,
                genero,
                poster_url,
                imdb_rating,
                sinopsis);

            _logger.LogInformation(
                "Película registrada: {PeliculaTitulo}, Género: {PeliculaGenero}",
                pelicula.titulo,
                pelicula.genero);

            return pelicula.Adapt<RegistrarPeliculaResponse>();
        }

        public async Task<ActualizarPeliculaResponse> ActualizarPelicula(
            Guid peliculaId,
            string titulo,
            int release_year,
            string genero,
            string poster_url,
            float imdb_rating,
            string sinopsis)
        {
            var pelicula = await _peliculaDomainService.ActualizarPelicula(
                peliculaId,
                titulo,
                release_year,
                genero,
                poster_url,
                imdb_rating,
                sinopsis);

            _logger.LogInformation(
                "Película actualizada con id {PeliculaId}",
                peliculaId);

            return pelicula.Adapt<ActualizarPeliculaResponse>();
        }

        public async Task<ConsultarPeliculaResponse> ConsultarPelicula(Guid peliculaId)
        {
            var pelicula = await _peliculaDomainService.ObtenerPelicula(peliculaId);

            _logger.LogInformation(
                "Película obtenida con id {PeliculaId}",
                peliculaId);

            return pelicula.Adapt<ConsultarPeliculaResponse>();
        }

        public IQueryable<Pelicula> ConsultarPeliculas()
        {
            var peliculas = _peliculaDomainService.ConsultarPeliculas();

            _logger.LogInformation("Consulta de películas realizada");

            return peliculas;
        }

        public async Task<EliminarPeliculaResponse> EliminarPelicula(Guid peliculaId)
        {
            var pelicula = await _peliculaDomainService.ObtenerPelicula(peliculaId);

            await _peliculaDomainService.EliminarPelicula(peliculaId);

            _logger.LogInformation(
                "Película eliminada con id {PeliculaId}",
                peliculaId);

            return pelicula.Adapt<EliminarPeliculaResponse>();
        }

        public async Task<RestaurarPeliculaResponse> RestaurarPelicula(Guid peliculaId)
        {
            // Primero obtenemos la película eliminada para poder retornar sus datos
            await _peliculaDomainService.RestaurarPelicula(peliculaId);
            var pelicula = await _peliculaDomainService.ObtenerPelicula(peliculaId);
            _logger.LogInformation($"Residente restaurado: {peliculaId}");
            return pelicula.Adapt<RestaurarPeliculaResponse>();
        }
    }
}