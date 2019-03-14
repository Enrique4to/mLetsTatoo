namespace mLetsTatoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class Tciudad
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cid_Estado { get; set; }
        [Required]
        public string Ciudad { get; set; }
        #endregion
    }
}
