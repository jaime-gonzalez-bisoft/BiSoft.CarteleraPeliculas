using System.ComponentModel.DataAnnotations;

namespace BiSoft.CarteleraPeliculas.Api.DTOs.Pelicula
{
    public class UpdateTituloRequest
    {
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;
    }

    public class UpdateReleaseYearRequest
    {
        [Required]
        public int ReleaseYear { get; set; }
    }

    public class UpdateGeneroRequest
    {
        [Required]
        [StringLength(50)]
        public string Genero { get; set; } = string.Empty;
    }

    public class UpdatePosterUrlRequest
    {
        [Required]
        [StringLength(1000)]
        public string PosterUrl { get; set; } = string.Empty;
    }

    public class UpdateImdbRatingRequest
    {
        [Required]
        [Range(0, 10)]
        public float ImdbRating { get; set; }
    }

    public class UpdateSinopsisRequest
    {
        [Required]
        [StringLength(2000)]
        public string Sinopsis { get; set; } = string.Empty;
    }
}
