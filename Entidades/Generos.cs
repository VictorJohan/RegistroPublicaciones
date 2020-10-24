using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroPublicaciones.Entidades
{
    public class Generos
    {
        [Key]
        public int GeneroId { get; set; }
        public string Gnero { get; set; }
    }
}
