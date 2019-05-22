using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mLetsTatoo.Models
{
    public class T_balanceempresa
    {
        [Key]
        public int Id_Balanceempresa { get; set; }
        [Required]
        public int Id_Empresa { get; set; }
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
