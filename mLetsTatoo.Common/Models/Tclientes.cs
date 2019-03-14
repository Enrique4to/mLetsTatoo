namespace mLetsTatoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class Tclientes
    {
        #region Properties
        [Key]
        public int Id_Cliente { get; set; }
        [Required]
        public string Nombre1 { get; set; }
        [Required]
        public string Nombre2 { get; set; }
        [Required]
        public string Apellido1 { get; set; }
        [Required]
        public string Apellido2 { get; set; }
        [Required]
        public string Correo { get; set; }
        public int Telefono { get; set; }
        [Required]
        public int Id_usuario { get; set; }
        [Required]
        public DateTime F_Nac { get; set; }
        public bool Bloqueo { get; set; }
        public byte[] F_Prefil { get; set; }
        #endregion
    }
}
