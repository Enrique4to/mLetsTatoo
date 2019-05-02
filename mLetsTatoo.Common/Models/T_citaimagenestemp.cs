namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;

    public class T_citaimagenestemp
    {
        #region Properties
        [Key]
        public int Id_Imagentemp { get; set; }
        [Required]
        public byte[] Imagen
        {
            get;
            set;
        }
        [Required]
        public int Id_Trabajotemp { get; set; }
        #endregion
    }
}
