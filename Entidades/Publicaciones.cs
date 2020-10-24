using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RegistroPublicaciones.Entidades
{
    public class Publicaciones
    {
        [Key]
        public int PublicacionId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int GeneroId { get; set; }
        public string NombreCancion { get; set; }
        public string Link { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public byte[] Wallpaper { get; set; }

        [ForeignKey("GeneroId")]
        public virtual Generos Genero { get; set; }

    }
}
