using System;
using System.ComponentModel.DataAnnotations;

namespace mLetsTatoo.Models
{
    public class T_trabajocitas
    {
        [Key]
        public int Id_Cita { get; set; }
        [Required]
        public int Id_Trabajo { get; set; }
        [Required]
        public DateTime F_Inicio { get; set; }
        [Required]
        public DateTime F_Fin { get; set; }
        [Required]
        public TimeSpan H_Inicio { get; set; }
        [Required]
        public TimeSpan H_Fin { get; set; }
        [Required]
        public string Asunto { get; set; }
        [Required]
        public bool Completa { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Tatuador { get; set; }
    }
}
