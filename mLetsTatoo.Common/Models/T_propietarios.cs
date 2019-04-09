using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class T_propietarios
    {
        [Key]
        public int Id_Propietario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido1 { get; set; }
        [Required]
        public string Apellido2 { get; set; }
        [Required]
        public string Id_Empresa { get; set; }
        [Required]
        public Int64 Tel_Contacto { get; set; }
    }
}
