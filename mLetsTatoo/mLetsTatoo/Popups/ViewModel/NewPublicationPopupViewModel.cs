namespace mLetsTatoo.Popups.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using ViewModels;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using System;
    using Xamarin.Forms;
    using Rg.Plugins.Popup.Extensions;
    using mLetsTatoo.Popups.Views;
    using System.IO;
    using Plugin.Media.Abstractions;
    using Plugin.Media;

    public class NewPublicationPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private TecnicosCollection tecnico;
        private ClientesCollection cliente;
        private EmpresasCollection empresa;
        private T_locales local;
        public List<T_imgpublicacion> imgList;
        #endregion

        #region Properties
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string EmpresaInfo { get; set; }
        public byte[] F_Perfil { get; set; }
        #endregion

        #region Constructors
        public NewPublicationPopupViewModel()
        {
            this.LoadUsers();
            this.apiService = new ApiService();
        }

        #endregion

        #region Commands

        #endregion

        #region Methods

        private void LoadUsers()
        {
            if (MainViewModel.GetInstance().Login.tecnico != null)
            {
                this.tecnico = MainViewModel.GetInstance().Login.tecnico;
                this.empresa = MainViewModel.GetInstance().Login.EmpresaList.Select(e => new EmpresasCollection
                {
                    Id_Empresa = e.Id_Empresa,
                    Bloqueo = e.Bloqueo,
                    Id_Usuario = e.Id_Usuario,
                    Nombre = e.Nombre,
                    F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.FirstOrDefault(u => u.Id_usuario == e.Id_Usuario).F_Perfil,

                }).Where(e => e.Id_Empresa == this.tecnico.Id_Empresa).FirstOrDefault();
                this.local = MainViewModel.GetInstance().Login.LocalesList.Where(l => l.Id_Local == this.tecnico.Id_Local).FirstOrDefault();

                this.Nombre = tecnico.Nombre;
                this.Apellido = tecnico.Apellido;
                this.F_Perfil = tecnico.F_Perfil;
                this.EmpresaInfo = $"({this.empresa.Nombre} {Languages.BranchOffice} {this.local.Nombre})";
            }
            else
            {
                this.cliente = MainViewModel.GetInstance().Login.cliente;
                this.Nombre = cliente.Nombre;
                this.Apellido = cliente.Apellido;
                this.F_Perfil = cliente.F_Perfil;
            }
        }
        #endregion
    }
}
