namespace BiSoft.CarteleraPeliculas.Application.DTOs
{
    public class ActualizarPeliculaResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public float ImdbRating { get; set; }
        public string Sinopsis { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}