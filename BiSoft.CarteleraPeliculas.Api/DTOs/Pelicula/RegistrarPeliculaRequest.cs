namespace BiSoft.CarteleraPeliculas.Api.DTOs.Pelicula
{
    public class RegistrarPeliculaRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public int Release_year { get; set; } = 0;
        public string Genero { get; set; } = string.Empty;
        public string Poster_url { get; set; } = string.Empty;
        public float Imdb_rating { get; set; } = 0;
        public string Sinopsis { get; set; } = string.Empty;

    }
}