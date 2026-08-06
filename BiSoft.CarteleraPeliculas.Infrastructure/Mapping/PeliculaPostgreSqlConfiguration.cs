using BiSoft.CarteleraPeliculas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiSoft.CarteleraPeliculas.Infrastructure.Mapping.PostgreSql;

public class PeliculaPostgreSqlConfiguration
    : IEntityTypeConfiguration<Pelicula>
{
    public void Configure(EntityTypeBuilder<Pelicula> builder)
    {
        builder.ToTable("peliculas");

        builder.HasKey(pelicula => pelicula.Id);

        builder.Property(pelicula => pelicula.Id)
            .HasColumnName("pelicula_id")
            .ValueGeneratedOnAdd();

        builder.Property(pelicula => pelicula.Titulo)
            .HasColumnName("titulo")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(pelicula => pelicula.AnioEstreno)
            .HasColumnName("anio_estreno")
            .IsRequired();

        builder.Property(pelicula => pelicula.Genero)
            .HasColumnName("genero")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(pelicula => pelicula.PosterUrl)
            .HasColumnName("poster_url")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(pelicula => pelicula.Calificacion)
            .HasColumnName("calificacion")
            .HasPrecision(3, 1)
            .IsRequired();

        builder.Property(pelicula => pelicula.Sinopsis)
            .HasColumnName("sinopsis")
            .HasMaxLength(2000)
            .IsRequired();

        builder.HasIndex(pelicula => pelicula.Titulo)
            .HasDatabaseName("ix_peliculas_titulo");

        builder.ToTable(tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "ck_peliculas_anio_estreno",
                "anio_estreno >= 1888 AND anio_estreno <= 2100");

            tableBuilder.HasCheckConstraint(
                "ck_peliculas_calificacion",
                "calificacion >= 0 AND calificacion <= 10");
        });
    }
}