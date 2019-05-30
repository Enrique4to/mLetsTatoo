namespace mLetsTatoo.Models
{
    using System.ComponentModel.DataAnnotations;
    public class T_datos_bancarios
    {
        [Key]
        public int Id_DatoBancario { get; set; }
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public string Cuenta { get; set; }
        [Required]
        public string Banco { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
