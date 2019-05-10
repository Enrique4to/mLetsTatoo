namespace mLetsTatoo.Models
{
    using System;

    public class TrabajonotaTempCollection
    {
        public int Id_Notatemp { get; set; }
        public int Id_Trabajotemp { get; set; }
        public int Tipo_Usuario { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Local { get; set; }
        public string Nota { get; set; }
        public DateTime F_nota { get; set; }
        public bool Propuesta { get; set; }
        public string Nombre_Post { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public byte[] F_Perfil { get; set; }
    }
}
