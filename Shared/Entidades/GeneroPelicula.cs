using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraBlazor.Shared.Entidades
{
    public class GeneroPelicula
    {
        public int peliculaId { get; set; }
        public int generoId { get; set; }
        public Genero? genero { get; set; }
        public Pelicula? pelicula { get; set; }
    }
}
