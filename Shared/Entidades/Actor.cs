using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteleraBlazor.Shared.Entidades
{
    public class Actor
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Campo {0} requerido")]
        public string nombre { get; set; } = null!;
        public string? biografia { get; set; }
        public string? foto { get; set; }
        public string? personaje { get; set; }
        public DateTime? fechaNancimiento { get; set; }
        public override bool Equals(object? obj)
        {
            if(obj is Actor actor)
            {
                return id == actor.id;
            }
            return false;    
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }
}
