using BiSoft.CarteleraPeliculas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteleraPeliculas.Infrastructure.Mapping.Sqlite;

public class PeliculaSqliteConfiguration
    : IEntityTypeConfiguration<Pelicula>
{
    public void Configure(EntityTypeBuilder<Pelicula> builder)
    {
        builder.ToTable("Peliculas");

        builder.HasKey(pelicula => pelicula.Id);

        builder.Property(pelicula => pelicula.Id)
            .HasColumnName("PeliculaId")
            .ValueGeneratedOnAdd();

        builder.Property(pelicula => pelicula.titulo)
            .HasColumnName("Titulo")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(pelicula => pelicula.release_year)
            .HasColumnName("AnioEstreno")
            .IsRequired();

        builder.Property(pelicula => pelicula.genero)
            .HasColumnName("Genero")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(pelicula => pelicula.poster_url)
            .HasColumnName("PosterUrl")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(pelicula => pelicula.imdb_rating)
            .HasColumnName("Calificacion")
            .HasConversion<double>()
            .IsRequired();

        builder.Property(pelicula => pelicula.sinopsis)
            .HasColumnName("Sinopsis")
            .HasMaxLength(2000)
            .IsRequired();

        builder.HasIndex(pelicula => pelicula.titulo)
            .HasDatabaseName("IX_Peliculas_Titulo");

        builder.ToTable(
            "Peliculas",
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Peliculas_AnioEstreno",
                    "AnioEstreno >= 1888 AND AnioEstreno <= 2100");

                tableBuilder.HasCheckConstraint(
                    "CK_Peliculas_Calificacion",
                    "Calificacion >= 0 AND Calificacion <= 10");
            });
    }
}