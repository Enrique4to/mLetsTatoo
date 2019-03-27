namespace mLetsTatoo.Models
{
    using Android;
    using Android.Graphics;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.IO;
    using System.Text;
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
        public Int64 Telefono { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public DateTime F_Nac { get; set; }
        public bool Bloqueo { get; set; }
        public byte[] F_Perfil { get; set; }
        #endregion
    }
}
