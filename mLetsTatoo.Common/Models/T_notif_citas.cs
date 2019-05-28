namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class T_notif_citas
    {
        [Key]
        public int Id_Notif_Cita { get; set; }
        [Required]
        public int Id_Notificacion { get; set; }
        public int Id_Cita { get; set; }
        public int Id_TrabajoTemp { get; set; }
        public int TipoNotif { get; set; }
    }
}
