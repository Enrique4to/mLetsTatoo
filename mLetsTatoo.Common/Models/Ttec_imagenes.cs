using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Ttec_imagenes
    {
        [Key]
        public int Id_Imagen { get; set; }
        [Required]
        public byte[] Imagen { get; set; }
        [Required]
        public int Id_Tecnico { get; set; }
        public string Descripcion { get; set; }
    }
}
