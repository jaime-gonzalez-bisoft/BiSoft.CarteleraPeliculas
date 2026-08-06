using CarteleraPeliculas.Infrastructure.Mapping.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace CarteleraPeliculas.Infrastructure.Helpers;

public static class ModelBuilderExtensions
{
    public static void ApplyCarteleraPeliculasConfigurations(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(
            new PeliculaPostgreSqlConfiguration());
    }
}