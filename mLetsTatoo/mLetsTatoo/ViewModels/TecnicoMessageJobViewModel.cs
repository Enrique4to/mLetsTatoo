﻿

namespace mLetsTatoo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Models;
    using mLetsTatoo.Services;
    using mLetsTatoo.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class TecnicoMessageJobViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string message;
        public bool pageVisible;
        private string anticipo;
        private string total;
        public string time;

        private TecnicosCollection tecnico;
        private T_usuarios user;
        private TrabajosTempItemViewModel trabajo;
        private T_trabajonotatemp notaTemp;
        private T_trabajostemp trabajoTemp;
        private TrabajonotaTempCollection nota;

        private ObservableCollection<NotasTempItemViewModel> notas;

        private bool isVisible;
        private bool isButtonEnabled;
        #endregion

        #region Properties
        public List<T_trabajonotatemp> TrabajoNotaList { get; set; }

        public ObservableCollection<NotasTempItemViewModel> Notas
        {
            get { return this.notas; }
            set { SetValue(ref this.notas, value); }
        }
        public TrabajosTempItemViewModel Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public string Anticipo
        {
            get { return this.anticipo; }
            set { SetValue(ref this.anticipo, value); }
        }
        public string Total
        {
            get { return this.total; }
            set { SetValue(ref this.total, value); }
        }
        public string Time
        {
            get { return this.time; }
            set { SetValue(ref this.time, value); }
        }
        public string Message
        {
            get { return this.message; }
            set { SetValue(ref this.message, value); }
        }
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsButtonEnabled
        {
            get { return this.isButtonEnabled; }
            set { SetValue(ref this.isButtonEnabled, value); }
        }
        #endregion

        #region Constructors
        public TecnicoMessageJobViewModel(TrabajosTempItemViewModel trabajo, TecnicosCollection tecnico, T_usuarios user, List<T_trabajonotatemp> TrabajoNotaList)
        {
            this.apiService = new ApiService();
            this.tecnico = tecnico;
            this.user = user;
            this.trabajo = trabajo;
            this.TrabajoNotaList = TrabajoNotaList;
            this.pageVisible = false;
            this.LoadListNotas();

            IsButtonEnabled = false;
            IsVisible = false;
            if (this.trabajo.Costo_Cita != 0)
                this.Anticipo = this.trabajo.Costo_Cita.ToString();

            if (this.trabajo.Total_Aprox != 0)
                this.Total = this.trabajo.Total_Aprox.ToString();

            if (this.trabajo.Tiempo != 0)
                this.Time = this.trabajo.Tiempo.ToString();
        }
        #endregion

        #region Commands
        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(SendMessage);
            }
        }
        public ICommand SendBudgetCommand
        {
            get
            {
                return new RelayCommand(SendBudget);
            }
        }
        #endregion

        #region Methods
        private async void SendMessage()
        {
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
            var controller = Application.Current.Resources["UrlT_trabajonotatempController"].ToString();

            var nombre_Post = $"{this.tecnico.Nombre} {this.tecnico.Apellido} {"'"}{this.tecnico.Apodo}{"'"}";

            this.notaTemp = new T_trabajonotatemp
            {
                Id_Trabajotemp = this.trabajo.Id_Trabajotemp,
                Tipo_Usuario = 2,
                Id_Usuario = this.tecnico.Id_Usuario,
                Id_Local = this.tecnico.Id_Local,
                Nota = this.Message,
                Nombre_Post = nombre_Post,
            };
            var response = await this.apiService.Post(urlApi, prefix, controller, this.notaTemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var newNota = (T_trabajonotatemp)response.Result;

            this.nota = new TrabajonotaTempCollection
            {
                Id_Notatemp = newNota.Id_Notatemp,
                Id_Trabajotemp = newNota.Id_Trabajotemp,
                Id_Usuario = newNota.Id_Usuario,
                Tipo_Usuario = newNota.Tipo_Usuario,
                F_nota = newNota.F_nota,
                Id_Local = newNota.Id_Local,
                Nota = newNota.Nota,

                Nombre = this.tecnico.Nombre,
                Apellido = this.tecnico.Apellido,

                F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.FirstOrDefault(u => u.Id_usuario == newNota.Id_Usuario).F_Perfil

            };

            MainViewModel.GetInstance().TecnicoMessages.TrabajoNotaList.Add(newNota);
            MainViewModel.GetInstance().TecnicoMessages.RefreshTrabajosList();
            this.LoadListNotas();
            this.Message = null;
        }
        private async void SendBudget()
        {
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
            var controller = Application.Current.Resources["UrlT_trabajostempController"].ToString();

            this.trabajoTemp = new T_trabajostemp
            {
                Id_Trabajotemp = this.trabajo.Id_Trabajotemp,
                Id_Tatuador = this.trabajo.Id_Tatuador,
                Id_Cliente = this.trabajo.Id_Cliente,
                Asunto = this.trabajo.Asunto,
                Costo_Cita = decimal.Parse(this.Anticipo),
                Total_Aprox = decimal.Parse(this.Total),
                Alto = this.trabajo.Alto,
                Ancho = this.trabajo.Ancho,
                Tiempo = int.Parse(this.Time),                
            };
            var response = await this.apiService.Put(urlApi, prefix, controller, this.trabajoTemp, this.trabajoTemp.Id_Trabajotemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            this.trabajoTemp = (T_trabajostemp)response.Result;
            var trabajoOld = MainViewModel.GetInstance().TecnicoMessages.TrabajosList.Where(t => t.Id_Trabajotemp == this.trabajo.Id_Trabajotemp).FirstOrDefault();
            if (trabajoOld != null)
            {
                MainViewModel.GetInstance().TecnicoMessages.TrabajosList.Remove(trabajoOld);
            }

            MainViewModel.GetInstance().TecnicoMessages.TrabajosList.Add(trabajoTemp);
            MainViewModel.GetInstance().TecnicoMessages.RefreshTrabajosList();

            this.Message = Languages.BudgetSentMessage;
            this.SendMessage();
        }
        public void LoadListNotas()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            var clienteList = MainViewModel.GetInstance().Login.ClienteList;

            var nota = this.TrabajoNotaList.Select(n => new NotasTempItemViewModel
            {
                Id_Notatemp = n.Id_Notatemp,
                Id_Trabajotemp = n.Id_Trabajotemp,
                Id_Usuario = n.Id_Usuario,

                Tipo_Usuario = n.Tipo_Usuario,
                F_nota = n.F_nota,
                Id_Local = n.Id_Local,
                Nota = n.Nota,

                Nombre =
                    n.Tipo_Usuario == 2 ?
                    this.tecnico.Nombre:

                    clienteList.FirstOrDefault(c => c.Id_Usuario == n.Id_Usuario).Nombre,

                Apellido = 
                    n.Tipo_Usuario == 2 ?
                    this.tecnico.Apellido :

                    clienteList.FirstOrDefault(c => c.Id_Usuario == n.Id_Usuario).Apellido,

                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == n.Id_Usuario).F_Perfil

            }).Where(n => n.Id_Trabajotemp == this.trabajo.Id_Trabajotemp).ToList();

            this.Notas = new ObservableCollection<NotasTempItemViewModel>(nota.OrderBy(n => n.F_nota));

            this.apiService.EndActivityPopup();
        }
        #endregion
    }
}
