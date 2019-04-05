﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class T_trabajocitas
    {
        [Key]
        public int Id_Cita { get; set; }
        [Required]
        public int Id_Trabajo { get; set; }
        [Required]
        public DateTime F_Inicio { get; set; }
        [Required]
        public DateTime F_Fin { get; set; }
    }
}
