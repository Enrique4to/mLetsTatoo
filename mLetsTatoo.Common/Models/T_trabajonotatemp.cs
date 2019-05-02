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
        public int Tipo_Usuario { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public int Id_Local { get; set; }
        [Required]
        public string Nota { get; set; }
        [Required]
        public DateTime F_nota { get; set; }
        [Required]
        public string Nombre_Post { get; set; }
    }
}
