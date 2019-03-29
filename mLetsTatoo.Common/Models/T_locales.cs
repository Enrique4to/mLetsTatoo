using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class T_locales
    {
        #region Properties
        [Key]
        public int Id_Local { get; set; }
        [Required]
        public int Id_Empresa { get; set; }
        [Required]
        public string Calle { get; set; }
        public string Numero { get; set; }
        public int Id_Colonia { get; set; }
        public int Id_Ciudad { get; set; }
        public int Id_Estado { get; set; }
        public int Id_Cpostal { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Referencia { get; set; }

        #endregion
    }
}
