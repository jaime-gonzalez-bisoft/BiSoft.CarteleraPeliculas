using BiSoft.CarteleraPeliculas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.CarteleraPeliculas.Domain.Repositories
{
    public interface IPeliculaRepository
    {
        Task RegistrarPelicula(Pelicula pelicula);
        Task GuardarCambios();
        Task<Pelicula> ObtenerPelicula(Guid peliculaId);
        IQueryable<Pelicula> ConsultarPelicula();
        Task<List<Pelicula>> ObtenerPeliculasEliminadas();
        Task RestaurarPelicula(Pelicula pelicula);
        Task EliminarPelicula(Pelicula pelicula);
    }
}
