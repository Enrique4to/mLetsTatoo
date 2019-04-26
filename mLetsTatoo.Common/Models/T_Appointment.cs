namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class T_Appointment
    {
        [Required]
        public int Id_Cita { get; set; }
        [Required]
        public int Id_Trabajo { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Tatuador { get; set; }
        [Required]
        public DateTime? F_Inicio { get; set; }
        [Required]
        public DateTime? F_Fin { get; set; }
        [Required]
        public TimeSpan? H_Inicio { get; set; }
        [Required]
        public TimeSpan? H_Fin { get; set; }
        [Required]
        public string Asunto { get; set; }
    }
}
