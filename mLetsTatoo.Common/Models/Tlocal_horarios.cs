using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Tlocal_horarios
    {
        #region Properties
        [Key]
        public int Id_Horario { get; set; }
        [Required]
        public int Id_Local { get; set; }
        public DateTime Hluvia { get; set; }
        public DateTime Hluvide { get; set; }
        public DateTime Hsaba { get; set; }
        public DateTime Hsabde { get; set; }        
        public bool Hluviact { get; set; }
        public bool Hsabact { get; set; }
        #endregion
    }
}
