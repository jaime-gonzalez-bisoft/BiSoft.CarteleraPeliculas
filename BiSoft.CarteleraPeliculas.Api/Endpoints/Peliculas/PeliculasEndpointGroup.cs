namespace BiSoft.CarteleraPeliculas.Api.Endpoints.Peliculas
{
    public static class PeliculasEndpointGroup
    {
        public static RouteGroupBuilder MapPeliculasEndpoints(this RouteGroupBuilder group)
        {
            var peliculasGroup = group.MapGroup("peliculas").WithTags("peliculas");
            peliculasGroup.MapEndpoints();
            return group;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapRegistrarPelicula();
            group.MapConsultarPeliculas();
            group.MapConsultarPelicula();
            group.MapEliminarPelicula();
            return group;
        }
    }
}
