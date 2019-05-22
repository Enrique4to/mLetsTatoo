namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class T_pagoscliente
    {
        [Key]
        public int Id_Pagocliente { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Trabajo { get; set; }
        [Required]
        public decimal Pago { get; set; }
        [Required]
        public bool Pagado { get; set; }
        [Required]
        public int Tipo_Pago { get; set; }
        [Required]
        public DateTime Fecha_Peticion { get; set; }
        [Required]
        public DateTime Fecha_Pago { get; set; }
    }
}
