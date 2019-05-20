namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class T_comentpublicacion
    {
        #region Properties
        [Key]
        public int Id_Comentario { get; set; }
        [Required]
        public int Id_Publicacion { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public DateTime Fecha_Comentario { get; set; }
        #endregion
    }
}
