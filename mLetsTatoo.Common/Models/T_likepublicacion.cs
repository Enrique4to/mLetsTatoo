namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class T_likepublicacion
    {
        #region Properties
        [Key]
        public int Id_Likepublicacion { get; set; }
        [Required]
        public int Id_Publicacion { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public bool Like { get; set; }
        #endregion
    }
}
