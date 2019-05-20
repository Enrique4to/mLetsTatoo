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
        private string publicacion;
        private TecnicosCollection tecnico;
        private ClientesCollection cliente;
        private EmpresasCollection empresa;
        private T_usuarios user;
        private T_locales local;
        public List<MediaFile> imgList;
        public MediaFile file;
        #endregion

        #region Properties
        public string Publicacion
        {
            get { return this.publicacion; }
            set { SetValue(ref this.publicacion, value); }
        }
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

        public ICommand NextCommand
        {
            get
            {
                return new RelayCommand(SavePublication);
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }
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
            this.user = MainViewModel.GetInstance().Login.user;
        }


        private async void SavePublication()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();

            this.apiService.StartActivityPopup();
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var newPublication = new T_publicaciones
            {
                Fecha_Publicacion = DateTime.Now.ToLocalTime(),
                Id_Usuario = this.user.Id_usuario,
                Publicacion = this.Publicacion,
                Modif_Date = DateTime.Now.ToLocalTime(),
            };
            var controller = Application.Current.Resources["UrlT_publicacionesController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, newPublication);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newPublication = (T_publicaciones)response.Result;

            MainViewModel.GetInstance().TecnicoHome.ListPublicaciones.Add(newPublication);
            controller = Application.Current.Resources["UrlT_imgpublicacionController"].ToString();

            if (this.imgList != null)
            {
                for (int i = 0; i < imgList.Count(); i++)
                {
                    var imgFile = imgList.ElementAtOrDefault(i);
                    var newImagen = new T_imgpublicacion
                    {
                        Id_Publicacion = newPublication.Id_Publicacion,
                        Id_Usuario = newPublication.Id_Usuario,
                        Imagen = FileHelper.ReadFully(imgFile.GetStream()),
                    };

                    response = await this.apiService.Post(urlApi, prefix, controller, newImagen);

                    if (!response.IsSuccess)
                    {
                        this.apiService.EndActivityPopup();

                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                        return;
                    }

                    newImagen = (T_imgpublicacion)response.Result;

                    MainViewModel.GetInstance().TecnicoHome.ListImgPublicacion.Add(newImagen);
                }
            }
            else if(this.file != null)
            {
                var newImagen = new T_imgpublicacion
                {
                    Id_Publicacion = newPublication.Id_Publicacion,
                    Id_Usuario = newPublication.Id_Usuario,
                    Imagen = FileHelper.ReadFully(this.file.GetStream()),
                };

                response = await this.apiService.Post(urlApi, prefix, controller, newPublication);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                newImagen = (T_imgpublicacion)response.Result;

                MainViewModel.GetInstance().TecnicoHome.ListImgPublicacion.Add(newImagen);
            }
            MainViewModel.GetInstance().TecnicoHome.RefreshPublicaciones();
            this.apiService.EndActivityPopup();
        }

        private void Cancel()
        {
            Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        #endregion
    }
}
