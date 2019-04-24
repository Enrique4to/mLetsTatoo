namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class T_empresas
    {
        #region Properties
        [Key]
        public int Id_Empresa { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Bloqueo { get; set; }
        #endregion

    }
}
