namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;
    public class T_postal
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Colonia { get; set; }
        [Required]
        public int Cpid_Estado { get; set; }
        [Required]
        public string Asentamiento { get; set; }
        [Required]
        public int Cpid_Asentamiento { get; set; }
        [Required]
        public int Cpid_Ciudad { get; set; }
        [Key]
        public int Cpid_Colonia { get; set; }
    }
}
