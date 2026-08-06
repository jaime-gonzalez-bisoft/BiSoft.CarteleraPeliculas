using BiSoft.CarteleraPeliculas.Domain.Entities;
using BiSoft.CarteleraPeliculas.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiSoft.CarteleraPeliculas.Domain.Services
{
    public class PeliculaDomainService
    {
        private readonly ILogger<PeliculaDomainService> _logger;
        private readonly IPeliculaRepository _peliculaRepository;

        public PeliculaDomainService(
            ILogger<PeliculaDomainService> logger,
            IPeliculaRepository peliculaRepository)
        {
            _logger = logger;
            _peliculaRepository = peliculaRepository;
        }

        public async Task<Pelicula> RegistrarPelicula
        (
            string titulo,
            int release_year,
            string genero,
            string poster_url,
            float imdb_rating,
            string sinopsis
        )
        {
            var pelicula = new Pelicula
                (
                    titulo,
                    release_year,
                    genero,
                    poster_url,
                    imdb_rating,
                    sinopsis
                );

            await _peliculaRepository.RegistrarPelicula(pelicula);
            await _peliculaRepository.GuardarCambios();

            _logger.LogInformation(
                $"Película registrada: {pelicula.Id}"
                );

            return pelicula;
        }

        public async Task<Pelicula> ActualizarPelicula
        (
            Guid peliculaId,
            string titulo,
            int release_year,
            string genero,
            string poster_url,
            float imdb_rating,
            string sinopsis
        )
        {
            var pelicula = await ObtenerPelicula(peliculaId);

            pelicula.Actualizar
            (
                titulo,
                release_year,
                genero,
                poster_url,
                imdb_rating,
                sinopsis
            );

            await _peliculaRepository.GuardarCambios();

            _logger.LogInformation(
                $"Película actualizada: {pelicula.Id}"
                );

            return pelicula;
        }

        public async Task<Pelicula> ObtenerPelicula(Guid peliculaId)
        {
            var pelicula = await _peliculaRepository.ObtenerPelicula(peliculaId)
                ?? throw new KeyNotFoundException($"No se encontró la película con id {peliculaId}");
            
            _logger.LogInformation($"Película obtenida: {pelicula.Id}");
            return pelicula;
        }

        public IQueryable<Pelicula> ConsultarPeliculas()
        {
            var peliculas = _peliculaRepository.ConsultarPelicula();

            _logger.LogInformation("Consulta de películas realizada.");

            return peliculas;
        }

        public async Task EliminarPelicula(Guid peliculaId)
        {
            var pelicula = await ObtenerPelicula(peliculaId);

            await _peliculaRepository.EliminarPelicula(pelicula);
            await _peliculaRepository.GuardarCambios();

            _logger.LogInformation(
                "Película eliminada: {PeliculaId}",
                peliculaId);
        }

        public async Task RestaurarPelicula(Guid peliculaId)
        {
            var peliculasEliminadas =
                await _peliculaRepository.ObtenerPeliculasEliminadas();

            var pelicula = peliculasEliminadas
                .FirstOrDefault(p => p.Id == peliculaId)
                ?? throw new KeyNotFoundException(
                    $"No se encontró una película eliminada con id {peliculaId}");

            await _peliculaRepository.RestaurarPelicula(pelicula);
            await _peliculaRepository.GuardarCambios();

            _logger.LogInformation(
                "Película restaurada: {PeliculaId}",
                peliculaId);
        }
    }
}