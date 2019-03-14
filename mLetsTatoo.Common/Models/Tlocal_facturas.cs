using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Tlocal_facturas
    {
        #region Properties
        [Key]
        public int Id_Lfactura { get; set; }
        [Required]
        public int Id_Local { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public int Id_Colonia { get; set; }
        [Required]
        public int Id_Ciudad { get; set; }
        [Required]
        public int Id_Estado { get; set; }
        [Required]
        public int Id_Pais { get; set; }
        [Required]
        public int Id_Cpostal { get; set; }
        [Required]
        public string Rfc { get; set; }
        [Required]
        public string Razon_Soclial { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public int Telefono { get; set; }
        #endregion
    }
}
