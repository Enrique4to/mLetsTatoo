﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class T_locatarios
    {
        #region Properties
        [Key]
        public int Id_Locatario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido1 { get; set; }
        [Required]
        public string Apellido2 { get; set; }
        [Required]
        public Int64 Tel_Contacto { get; set; }
        [Required]
        public int Id_Local { get; set; }


        #endregion
    }
}
