namespace mLetsTatoo.Models
{
    using System;

    public class TrabajoCitasCollection
    {
        #region Citas
        public int Id_Cita { get; set; }
        public int Id_Trabajo { get; set; }
        public DateTime F_Inicio { get; set; }
        public TimeSpan H_Inicio { get; set; }
        public DateTime F_Fin { get; set; }
        public TimeSpan H_Fin { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Tecnico { get; set; }
        #endregion

        #region Trabajos
        public string Asunto { get; set; }
        public int Id_Caract { get; set; }
        public decimal Total_Aprox { get; set; }
        public decimal Costo_Cita { get; set; }
        public decimal Ancho { get; set; }
        public decimal Alto { get; set; }
        public int Tiempo { get; set; }
        #endregion

        #region Usuario
        public byte[] F_Perfil { get; set; }
        #endregion

        #region Cliente
        public string NombreCte { get; set; }
        public string ApellidoCte { get; set; }
        #endregion

        #region Tecnico
        public string NombreTco { get; set; }
        public string ApellidoTco { get; set; }
        #endregion

        #region ImagenTrabajo
        public byte[] Imagen { get; set; }
        #endregion

        #region Empresa
        public string NombreEmp { get; set; }
        #endregion

        #region Local
        public string NombreLocal { get; set; }

        #endregion
    }
}
