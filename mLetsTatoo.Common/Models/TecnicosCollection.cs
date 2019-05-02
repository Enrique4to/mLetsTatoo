namespace mLetsTatoo.Models
{
    public class TecnicosCollection
    {
        public int Id_Tecnico { get; set; }
        public int Id_Local { get; set; }
        public int Id_Empresa { get; set; }
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Apellido2 { get; set; }
        public string Apodo { get; set; }
        public string Carrera { get; set; }
        public byte[] F_Perfil { get; set; }
    }
}
