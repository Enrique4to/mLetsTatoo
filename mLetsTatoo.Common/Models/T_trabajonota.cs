using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class T_trabajonota
    {
        [Key]
        public int Id_Nota { get; set; }
        [Required]
        public int Id_Trabajo { get; set; }
        [Required]
        public int Id_Tatuador { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Local { get; set; }
        [Required]
        public string Nota { get; set; }
        [Required]
        public DateTime F_nota { get; set; }
    }
}
