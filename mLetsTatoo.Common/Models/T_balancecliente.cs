using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mLetsTatoo.Models
{
    public class T_balancecliente
    {
        [Key]
        public int Id_Balancecliente { get; set; }
        [Required]
        public int Id_Cliente { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public decimal Saldo_Favor { get; set; }
    }
}
