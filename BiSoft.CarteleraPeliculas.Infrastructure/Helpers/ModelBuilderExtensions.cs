using CarteleraPeliculas.Infrastructure.Mapping.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CarteleraPeliculas.Infrastructure.Helpers;

public static class ModelBuilderExtensions
{
    public static void ApplyCarteleraPeliculasConfigurations(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(
            new PeliculaSqliteConfiguration());
    }
}