

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

    public class UserMessageJobViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string message;
        public bool pageVisible;

        private ClientesCollection cliente;
        private T_usuarios user;
        private TrabajosTempItemViewModel trabajo;
        private T_trabajonotatemp notaTemp;
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
        public UserMessageJobViewModel(TrabajosTempItemViewModel trabajo, ClientesCollection cliente, T_usuarios user, List<T_trabajonotatemp> TrabajoNotaList)
        {
            this.apiService = new ApiService();
            this.cliente = cliente;
            this.user = user;
            this.trabajo = trabajo;
            this.TrabajoNotaList = TrabajoNotaList;
            this.pageVisible = false;
            this.LoadListNotas();

            IsButtonEnabled = false;
            IsVisible = false;
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

            var nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}";

            this.notaTemp = new T_trabajonotatemp
            {
                Id_Trabajotemp = this.trabajo.Id_Trabajotemp,
                Tipo_Usuario = 1,
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Local = MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(u => u.Id_Tecnico == this.trabajo.Id_Tatuador).Id_Local,
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

                Nombre = this.cliente.Nombre,
                Apellido = this.cliente.Apellido,

                F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.FirstOrDefault(u => u.Id_usuario == newNota.Id_Usuario).F_Perfil

            };

            MainViewModel.GetInstance().UserMessages.TrabajoNotaList.Add(newNota);
            MainViewModel.GetInstance().UserMessages.RefreshTrabajosList();
            this.LoadListNotas();
            this.Message = null;
        }
        public void LoadListNotas()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;

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
                    n.Tipo_Usuario == 1 ?
                    this.cliente.Nombre:

                    MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Usuario == n.Id_Usuario).Nombre,

                Apellido = 
                    n.Tipo_Usuario == 1 ?
                    this.cliente.Apellido :

                    MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Usuario == n.Id_Usuario).Apellido,

                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == n.Id_Usuario).F_Perfil

            }).Where(n => n.Id_Trabajotemp == this.trabajo.Id_Trabajotemp).ToList();

            this.Notas = new ObservableCollection<NotasTempItemViewModel>(nota.OrderBy(n => n.F_nota));
        }
        #endregion
    }
}
