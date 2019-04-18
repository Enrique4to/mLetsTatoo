namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class T_citaimagenes
    {
        #region Properties
        [Key]
        public int Id_Imagen { get; set; }
        [Required]
        public byte[] Imagen
        {
            get;
            set;
        }
        [Required]
        public int Id_Trabajo { get; set; }
        #endregion
    }
}
