using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class T_trabajos
    {
        [Key]
        public int Id_Trabajo { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Tatuador { get; set; }
        [Required]
        public string Asunto { get; set; }
    }
}
