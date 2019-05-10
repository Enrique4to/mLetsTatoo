namespace mLetsTatoo.Models
{
    using System;

    public class TrabajoNotaCollection
    {
        public int Id_Nota { get; set; }
        public int Id_Trabajo { get; set; }
        public int Tipo_Usuario { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Local { get; set; }
        public string Nota { get; set; }
        public DateTime F_nota { get; set; }
        public int Id_Cita { get; set; }
        public string Nombre_Post { get; set; }
        public bool Cambio_Fecha { get; set; }
        public byte[] F_Perfil { get; set; }
    }
}
