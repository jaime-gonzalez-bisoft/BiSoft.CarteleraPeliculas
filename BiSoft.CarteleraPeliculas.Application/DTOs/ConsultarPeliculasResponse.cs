namespace BiSoft.CarteleraPeliculas.Application.DTOs
{
    public class ConsultarPeliculasResponse
    {
        public IEnumerable<ConsultarPeliculaResponse> Peliculas { get; set; } = [];
    }
}