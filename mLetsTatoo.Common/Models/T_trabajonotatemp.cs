namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class T_trabajonotatemp
    {
        [Key]
        public int Id_Notatemp { get; set; }
        [Required]
        public int Id_Trabajotemp { get; set; }
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
