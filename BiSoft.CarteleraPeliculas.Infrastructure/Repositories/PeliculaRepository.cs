using CarteleraPeliculas.Domain.Entities;
using CarteleraPeliculas.Domain.Repositories;
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

    public IQueryable<Pelicula> Consultar()
    {
        return _context.Peliculas
            .AsNoTracking();
    }

    public async Task<Pelicula?> ObtenerPorId(
        int peliculaId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Peliculas
            .FirstOrDefaultAsync(
                pelicula => pelicula.Id == peliculaId,
                cancellationToken);
    }

    public async Task Registrar(
        Pelicula pelicula,
        CancellationToken cancellationToken = default)
    {
        await _context.Peliculas.AddAsync(
            pelicula,
            cancellationToken);
    }

    public void Actualizar(Pelicula pelicula)
    {
        _context.Peliculas.Update(pelicula);
    }

    public void Eliminar(Pelicula pelicula)
    {
        _context.Peliculas.Remove(pelicula);
    }

    public async Task GuardarCambios(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}