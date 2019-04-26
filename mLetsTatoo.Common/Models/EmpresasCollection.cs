namespace mLetsTatoo.Models
{
    public class EmpresasCollection
    {
        #region Properties
        public int Id_Empresa { get; set; }
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; }
        public bool Bloqueo { get; set; }
        public byte[] F_Perfil { get; set; }
        #endregion
    }
}
