using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.CarteleraPeliculas.Domain.Entities
{
    public class Pelicula
    {
        Guid Id { get; }
        public string titulo { get; private set; }
        public int release_year { get; private set; }
        public string genero { get; private set; }
        public string poster_url { get; private set; }
        public float imdb_rating { get; private set; }
        public string sinopsis { get; private set; }
        public bool IsDeleted { get; set; } = false;
        private DateTime? _deletedAt;
        public DateTime? DeletedAt
        {
            get => _deletedAt;
            set => _deletedAt = value;
        }
        private Pelicula() { }
        public Pelicula
            (
                string titulo,
                int release_year,
                string genero,
                string poster_url,
                float imdb_rating,
                string sinopsis
            )
        {
            this.Id= Guid.NewGuid();
            this.titulo = titulo;
            this.release_year = release_year;
            this.genero = genero;
            this.poster_url = poster_url;
            this.imdb_rating = imdb_rating;
            this.sinopsis = sinopsis;
            this.IsDeleted = false;
        }
        public void Actualizar
            (
                string titulo,
                int release_year,
                string genero,
                string poster_url,
                float imdb_rating,
                string sinopsis
            ) 
        {
            
            this.titulo = titulo;
            this.release_year = release_year;
            this.genero = genero;
            this.poster_url = poster_url;
            this.imdb_rating = imdb_rating;
            this.sinopsis = sinopsis;
            this.IsDeleted = false;
        }
        // Metodos para Soft Delete
        public void Eliminar()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restaurar()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
