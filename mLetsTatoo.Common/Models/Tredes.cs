using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Tredes
    {
        [Key]
        public int Id_Redes { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        [Required]
        public int Id_Local { get; set; }
    }
}
