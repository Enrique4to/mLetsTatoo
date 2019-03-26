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
        [StringLength(100)]
        public string Nombre { get; set; }
        public byte[] Logo
        {
            get;
            set;
        }
        public bool Bloqueo { get; set; }
        #endregion

    }
}
