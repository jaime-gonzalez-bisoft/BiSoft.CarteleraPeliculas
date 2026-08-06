using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiSoft.CarteleraPeliculas.Domain.Entities;
using BiSoft.CarteleraPeliculas.Domain.Repositories;
using CarteleraPeliculas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarteleraPeliculas.Infrastructure.Repositories;

public class PeliculaRepository : IPeliculaRepository
{
    private readonly CarteleraPeliculasContext _context;

    public PeliculaRepository(CarteleraPeliculasContext context)
    {
        _context = context;
    }

    public IQueryable<Pelicula> ConsultarPelicula()
    {
        return _context.Peliculas.AsNoTracking();
    }

    public async Task<Pelicula> ObtenerPelicula(Guid peliculaId)
    {
        var pelicula = await _context.Peliculas
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == peliculaId);

        if (pelicula is null)
            throw new KeyNotFoundException($"Pelicula con id '{peliculaId}' no encontrada.");

        return pelicula;
    }

    public async Task RegistrarPelicula(Pelicula pelicula)
    {
        await _context.Peliculas.AddAsync(pelicula);
    }

    public Task RestaurarPelicula(Pelicula pelicula)
    {
        // Se asume que la entidad ya refleja el estado restaurado o el caller llama a `pelicula.Restaurar()`.
        _context.Peliculas.Update(pelicula);
        return Task.CompletedTask;
    }

    public Task EliminarPelicula(Pelicula pelicula)
    {
        _context.Peliculas.Remove(pelicula);
        return Task.CompletedTask;
    }

    public async Task GuardarCambios()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<Pelicula>> ObtenerPeliculasEliminadas()
    {
        return await _context.Peliculas
            .AsNoTracking()
            .Where(p => p.IsDeleted)
            .ToListAsync();
    }
}