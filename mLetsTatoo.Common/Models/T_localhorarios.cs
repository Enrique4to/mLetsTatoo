namespace mLetsTatoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class T_localhorarios
    {
        #region Properties
        [Key]
        public int Id_Horario { get; set; }
        [Required]
        public int Id_Local { get; set; }
        public TimeSpan Hluvia { get; set; }
        public TimeSpan Hluvide { get; set; }
        public TimeSpan Hsaba { get; set; }
        public TimeSpan Hsabde { get; set; }
        public TimeSpan Hdoma { get; set; }
        public TimeSpan Hdomde { get; set; }
        public bool Hluviact { get; set; }
        public bool Hsabact { get; set; }
        public bool Hdomact { get; set; }
        #endregion
    }
}
