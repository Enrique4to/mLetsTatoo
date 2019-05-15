namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class T_imgpublicacion
    {
        #region Properties
        [Key]
        public int Id_Imgpublicacion { get; set; }
        [Required]
        public int Id_Publicacion { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public byte[] Imagen { get; set; }
        #endregion
    }
}
