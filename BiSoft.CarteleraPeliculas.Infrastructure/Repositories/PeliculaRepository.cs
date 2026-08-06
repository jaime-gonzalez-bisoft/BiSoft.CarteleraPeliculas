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
        return _context.Peliculas
            .Where(p => !p.IsDeleted)
            .AsNoTracking();
    }

    public async Task<Pelicula> ObtenerPelicula(Guid peliculaId)
    {
        var pelicula = await _context.Peliculas
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == peliculaId && !p.IsDeleted);

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

    public async Task GuardarCambios()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException?.Message);
            throw;
        }
    }

    public async Task<List<Pelicula>> ObtenerPeliculasEliminadas()
    {
        return await _context.Peliculas
            .AsNoTracking()
            .Where(p => p.IsDeleted)
            .ToListAsync();
    }

    public Task EliminarPelicula(Pelicula pelicula)
    {
        pelicula.Eliminar();

        _context.Peliculas.Update(pelicula);

        return Task.CompletedTask;
    }
}