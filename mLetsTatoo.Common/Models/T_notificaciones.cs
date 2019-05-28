namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class T_notificaciones
    {
        [Key]
        public int  Id_Notificacion { get; set; }
        [Required]
        public int Usuario_Envia { get; set; }
        [Required]
        public int Usuario_Recibe { get; set; }
        [Required]
        public string Notificacion { get; set; }
        [Required]
        public bool Visto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}
