using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Tcita_imagenes
    {
        #region Properties
        [Key]
        public int Id_Imagen { get; set; }
        [Required]
        public int Id_Cita { get; set; }
        [Required]
        public byte[] Imagen { get; set; }
        public string Descripcion { get; set; }
        #endregion
    }
}
