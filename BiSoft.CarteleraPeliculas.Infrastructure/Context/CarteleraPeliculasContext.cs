using CarteleraPeliculas.Domain.Entities;
using CarteleraPeliculas.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CarteleraPeliculas.Infrastructure.Contexts;

public class CarteleraPeliculasContext : DbContext
{
    public CarteleraPeliculasContext(
        DbContextOptions<CarteleraPeliculasContext> options)
        : base(options)
    {
    }

    public DbSet<Pelicula> Peliculas => Set<Pelicula>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyCarteleraPeliculasConfigurations();
    }
}