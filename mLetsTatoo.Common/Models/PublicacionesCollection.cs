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
        public List<T_comentpublicacion> ListComentPublicacion { get; set; }
        public List<T_imgpublicacion> ListImgPublicacion { get; set; }
        //public T_comentpublicacion ComentPublicacion { get; set; }
        //public T_imgpublicacion ImgPublicacion { get; set; }
        //#endregion
        #endregion
    }
    //public class ComentPublicacion
    //{
    //    #region Properties
    //    public int Id_Comentario { get; set; }
    //    public int Id_Publicacion { get; set; }
    //    public int Id_Usuario { get; set; }
    //    public string Comentario { get; set; }
    //    public DateTime Fecha_Comentario { get; set; }
    //    #endregion
    //}
    //public class ImgPublicacion
    //{
    //    #region Properties
    //    public int Id_Imgpublicacion { get; set; }
    //    public int Id_Publicacion { get; set; }
    //    public int Id_Usuario { get; set; }
    //    public byte[] Imagen { get; set; }
    //    
    //}
    #endregion
}
