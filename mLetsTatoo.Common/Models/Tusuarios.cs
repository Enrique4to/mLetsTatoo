using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Tusuarios
    {
        #region Properties
        [Key]
        public int Id_usuario { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Pass { get; set; }
        public int Tipo { get; set; }
        public int Id_empresa { get; set; }
        public int Confirmacion { get; set; }
        public bool Bloqueo { get; set; }
        public bool Confirmado { get; set; }
        [Required]
        public string Ucorreo { get; set; }
        #endregion
        public override string ToString()
        {
            return this.Usuario;
        }

    }
}
