using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mLetsTatoo.Models
{
    public class Testado
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public string Estado { get; set; }
        #endregion
    }
}
