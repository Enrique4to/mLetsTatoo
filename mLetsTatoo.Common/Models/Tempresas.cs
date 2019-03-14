

namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Tempresas
    {
        #region Properties
        [Key]
        public int Id_Empresa { get; set; }
        [Required]
        public string Nombre { get; set; }
        public byte[] Logo { get; set; }
        public bool Bloqueo { get; set; } 
        #endregion
    }
}
