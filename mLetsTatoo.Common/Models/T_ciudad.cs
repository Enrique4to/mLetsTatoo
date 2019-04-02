
namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;
    public class T_ciudad
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cid_Ciudad { get; set; }
        [Required]
        public int Cid_Estado { get; set; }
        [Required]
        public string Ciudad { get; set; }
        #endregion
    }
}
