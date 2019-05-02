namespace mLetsTatoo.Models
{
    using System;
    public class TrabajosTempCollection
    {
        public int Id_Trabajotemp { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Tatuador { get; set; }
        public string Asunto { get; set; }
        public decimal Total_Aprox { get; set; }
        public decimal Costo_Cita { get; set; }
        public decimal Alto { get; set; }
        public decimal Ancho { get; set; }
        public int Tiempo { get; set; }

        public string Nota { get; set; }
        public DateTime F_nota { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
        public byte[] F_Perfil { get; set; }

        public byte[] Imagen { get; set; }
    }
}
