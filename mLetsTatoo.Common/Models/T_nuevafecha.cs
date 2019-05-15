namespace mLetsTatoo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class T_nuevafecha
    {
        #region Properties
        [Key]
        public int Id_nuevafecha { get; set; }
        [Required]
        public DateTime Nueva_Fecha { get; set; }
        [Required]
        public int Id_Cita { get; set; }
        #endregion
    }
}
