namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class T_retiros
    {
        [Key]
        public int Id_Retiros { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public decimal Retiro { get; set; }
        [Required]
        public DateTime Fecha_Retiro { get; set; }
    }
}
