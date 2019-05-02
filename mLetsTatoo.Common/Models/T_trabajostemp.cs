namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class T_trabajostemp
    {
        [Key]
        public int Id_Trabajotemp { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Tatuador { get; set; }
        [Required]
        public string Asunto { get; set; }
        [Required]
        public decimal Total_Aprox { get; set; }
        [Required]
        public decimal Costo_Cita { get; set; }
        [Required]
        public decimal Alto { get; set; }
        [Required]
        public decimal Ancho { get; set; }
        [Required]
        public int Tiempo { get; set; }
    }
}
