using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraBlazor.Shared.Entidades
{
    public class VotoPelicula
    {
        public int id { get; set; }
        public int voto { get; set; }
        public DateTime fechaVoto { get; set; }
        public int peliculaId { get; set; }
        public Pelicula? pelicula { get; set; }
    }
}
