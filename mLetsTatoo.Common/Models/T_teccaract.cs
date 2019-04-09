namespace mLetsTatoo.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class T_teccaract
    {
        #region Properties
        [Key]
        public int Id_Caract { get; set; }
        [Required]
        public int Id_Tecnico { get; set; }
        [Required]
        public string Caract { get; set; }
        [Required]
        public decimal Total_Aprox { get; set; }
        [Required]
        public decimal Costo_Cita { get; set; }
        [Required]
        public byte[] Imagen_Ejemplo { get; set; }
        #endregion
    }
}
