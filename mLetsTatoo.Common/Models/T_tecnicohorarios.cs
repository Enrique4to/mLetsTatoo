namespace mLetsTatoo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class T_tecnicohorarios
    {
        #region Properties
        [Key]
        public int Id_Horario { get; set; }
        [Required]
        public int Id_Tecnico { get; set; }
        public TimeSpan Hluvia { get; set; }
        public TimeSpan Hluvide { get; set; }
        public TimeSpan Hsaba { get; set; }
        public TimeSpan Hsabde { get; set; }
        public TimeSpan Hdoma { get; set; }
        public TimeSpan Hdomde { get; set; }
        public TimeSpan Hlvcomidaa { get; set; }
        public TimeSpan Hlvcomidade { get; set; }
        public TimeSpan Hscomidaa { get; set; }
        public TimeSpan Hscomidade { get; set; }
        public TimeSpan Hdcomidaa { get; set; }
        public TimeSpan Hdcomidade { get; set; }
        public bool Hluviact { get; set; }
        public bool Hsabact { get; set; }
        public bool Hdomact { get; set; }
        public bool Hlvcomidaact { get; set; }
        public bool Hscomidaact { get; set; }
        public bool Hdcomidaact { get; set; }
        #endregion
    }
}
