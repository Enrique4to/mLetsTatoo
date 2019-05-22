namespace mLetsTatoo.Models
{
    using System;

    public class ClientesCollection
    {
        #region Properties
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime F_Nac { get; set; }
        public bool Bloqueo { get; set; }
        public byte[] F_Perfil { get; set; }
        public decimal Saldo_Favor { get; set; }
        #endregion
    }
}
