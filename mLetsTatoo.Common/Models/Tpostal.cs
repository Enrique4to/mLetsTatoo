namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Tpostal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Colonia { get; set; }
        [Required]
        public int Cpid_Estado { get; set; }
        [Required]
        public int Cpid_Asentamiento { get; set; }
        [Required]
        public int Cpid_Ciudad { get; set; }
        [Required]
        public int Cpid_Colonia { get; set; }
    }
}
