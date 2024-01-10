using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraBlazor.Shared.Entidades
{
    public class Pelicula
    {
        public int id { get; set; }
        public string? resumen { get; set; }
        public bool enCartelera { get; set; }
        public string? trailer { get; set; }
        [Required]
        public string? titulo { get; set; }
        public DateTime? fechaLanzamiento { get; set; }
        public string? poster { get; set; }

        public List<GeneroPelicula> generos { get; set; } = new List<GeneroPelicula>();
        public List<ActorPelicula> actores { get; set; } = new List<ActorPelicula>();
        public string? tituloCorto
        {
            get
            {
                if (titulo == null)
                {
                    return null;
                }
                if (titulo.Length > 60)
                {
                    return titulo.Substring(0, 60) + "...";
                }
                return titulo;
            }
        }
    }
}
