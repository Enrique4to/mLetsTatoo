namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class T_publicaciones
    {
        #region Properties
        [Key]
        public int Id_Publicacion { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public string Publicacion { get; set; }
        [Required]
        public DateTime Fecha_Publicacion { get; set; }
        [Required]
        public DateTime Modif_Date { get; set; }
        #endregion
    }
}
