using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Documents;

namespace RegistroPublicaciones.Entidades
{
    public class Publicaciones
    {
        [Key]
        public int PublicacionId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
        public string Link { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
