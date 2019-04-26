using GalaSoft.MvvmLight.Command;
using mLetsTatoo.Helpers;
using mLetsTatoo.Models;
using mLetsTatoo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mLetsTatoo.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        #region Services
        private bool isRefreshing;
        private bool isRunning;
        private ApiService apiService;
        public string filter;
        #endregion

        #region Attributes
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        #endregion

        #region Properties
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshTecnicoList();
            }
        }
        public List<T_tecnicos> TecnicoList { get; set; }
        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public SearchViewModel()
        {
            this.apiService = new ApiService();
            this.LoadTecnicos();
            this.IsRunning = false;
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshTecnicoList);
            }
        }

        public ICommand RefreshArtistCommand
        {
            get
            {
                return new RelayCommand(LoadTecnicos);
            }
        }
        #endregion

        #region Methods

        private async void LoadTecnicos()
        {
            this.IsRefreshing = true;
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;

            }

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_tecnicosController"].ToString();

            var response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.TecnicoList = (List<T_tecnicos>)response.Result;
            this.IsRefreshing = false;
            this.IsRunning = false;

            this.RefreshTecnicoList();
        }
        public void RefreshTecnicoList()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            if (string.IsNullOrEmpty(this.filter))
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido1 = t.Apellido1,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                    F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == t.Id_Usuario).F_Perfil

                }).Where(t => userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)).ToList();
                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }
            else
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido1 = t.Apellido1,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                    F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == t.Id_Usuario).F_Perfil

                }).Where(t => userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)
                && (t.Nombre.ToLower().Contains(this.filter.ToLower())
                || t.Apodo.ToLower().Contains(this.filter.ToLower())
                || t.Apellido1.ToLower().Contains(this.filter.ToLower()))).ToList();

                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }

        }
        #endregion
    }
}
