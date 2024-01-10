using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraBlazor.Shared.Entidades
{
    public class ActorPelicula
    {
        public int actorId { get; set; }
        public int peliculaId { get; set; }

        public Pelicula? pelicula { get; set; }= new Pelicula();
        public Actor? actor { get; set; }= new Actor();

        public string? personaje { get; set;} = null!;
        public int orden { get; set; }
    }
}
