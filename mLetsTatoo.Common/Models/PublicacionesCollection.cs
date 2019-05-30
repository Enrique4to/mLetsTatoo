using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace mLetsTatoo.Models
{
    public class PublicacionesCollection
    {
        #region Properties
        #region Publicaciones
        public int Id_Publicacion { get; set; }
        public int Id_Usuario { get; set; }
        public string Publicacion { get; set; }
        public DateTime Fecha_Publicacion { get; set; }
        public DateTime Modif_Date { get; set; }
        #endregion

        #region Usuarios
        public int Tipo { get; set; }
        public byte[] F_Perfil { get; set; }
        #endregion

        #region ClientesTecnicos
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        #endregion

        #region Lists
        public List<T_comentpublicacion> Comentarios { get; set; }
        public List<T_imgpublicacion> Imagenes { get; set; }
        #endregion
    }
    #endregion
}
