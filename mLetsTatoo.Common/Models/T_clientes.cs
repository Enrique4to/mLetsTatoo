namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class T_clientes
    {
        #region Properties
        [Key]
        public int Id_Cliente { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public DateTime F_Nac { get; set; }
        public bool Bloqueo { get; set; }
        #endregion
    }
}
