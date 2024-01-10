﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraBlazor.Shared.Entidades
{
    public class Genero
    {
        public int id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string descripcion { get; set; }

        public List<GeneroPelicula> GenerosPelicula { get; set; } = new List<GeneroPelicula>();
    }
}
