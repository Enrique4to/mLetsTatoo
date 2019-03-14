using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Tlocal_historia
    {
        #region Properties
        [Key]
        public int Id_Historia { get; set; }
        [Required]
        public int Id_Local { get; set; }
        [Required]
        public string Historia { get; set; }
        #endregion
    }
}
