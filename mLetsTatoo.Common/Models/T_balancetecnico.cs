using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mLetsTatoo.Models
{
    public class T_balancetecnico
    {
        [Key]
        public int Id_Balancetecnico { get; set; }
        [Required]
        public int Id_Tecnico { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public decimal Saldo_Favor { get; set; }
        [Required]
        public decimal Saldo_Contra { get; set; }
        [Required]
        public decimal Saldo_Retenido { get; set; }
    }
}
