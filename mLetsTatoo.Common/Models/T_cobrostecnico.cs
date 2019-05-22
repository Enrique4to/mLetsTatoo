namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class T_cobrostecnico
    {
        [Key]
        public int Id_Cobrotecnico { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public int Id_Tecnico { get; set; }
        [Required]
        public decimal Pago { get; set; }
        [Required]
        public DateTime Fecha_Pago { get; set; }
        [Required]
        public string Concepto { get; set; }

    }
}
