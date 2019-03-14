using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Ttecnicos
    {
        [Key]
        public int Id_Tecnico { get; set; }
        [Required]
        public int Id_Local { get; set; }
        [Required]
        public int Id_Empresa { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido1 { get; set; }
        [Required]
        public string Apellido2 { get; set; }
        public string Apodo { get; set; }
        public string Carrera { get; set; }
        public byte[] F_Perfil { get; set; }
    }
}
