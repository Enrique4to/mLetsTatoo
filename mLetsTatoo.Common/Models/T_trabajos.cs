namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;
    public class T_trabajos
    {
        [Key]
        public int Id_Trabajo { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Tatuador { get; set; }
        [Required]
        public string Asunto { get; set; }
        public int? Id_Caract { get; set; }
        [Required]
        public decimal Total_Aprox { get; set; }
        [Required]
        public decimal Costo_Cita { get; set; }
        [Required]
        public decimal Ancho { get; set; }
        [Required]
        public decimal Alto { get; set; }
        [Required]
        public int Tiempo { get; set; }
        [Required]
        public bool Completo { get; set; }
        [Required]
        public bool Cancelado { get; set; }


    }
}
