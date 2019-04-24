namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LocalViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isRefreshing;
        public T_locales local;
        public T_estado estado;
        public T_ciudad ciudad;
        public T_postal postal;
        public T_empresas empresa;
        public T_usuarios empresaUser;
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private string address;
        #endregion

        #region Properties
        public string Address
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }
        public T_locales Local
        {
            get { return this.local; }
            set { SetValue(ref this.local, value); }
        }
        public T_empresas Empresa
        {
            get { return this.empresa; }
            set { SetValue(ref this.empresa, value); }
        }
        public T_usuarios EmpresaUser
        {
            get { return this.empresaUser; }
            set { SetValue(ref this.empresaUser, value); }
        }
        public List<T_tecnicos> LocalTecnicoList { get; set; }
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
        public LocalViewModel(T_locales local, T_empresas empresa)
        {
            this.local = local;
            this.empresa = empresa;
            this.apiService = new ApiService();
            Task.Run(async () => { await this.LoadInfo(); }).Wait();
            this.LoadTecnicos();
            this.IsRefreshing = false;
            this.IsRunning = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshArtistCommand
        {
            get
            {
                return new RelayCommand(LoadTecnicos);
            }
        }
        #endregion

        #region Methods
        public async Task LoadInfo()
        {
            this.IsRunning = true;
            this.IsRefreshing = true;
            if (string.IsNullOrEmpty(address))
            {
                var connection = await this.apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    this.IsRefreshing = false;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        connection.Message,
                        "OK");
                    return;
                }
                //-----------------Cargar Datos Postal-----------------//

                var urlApi = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlT_postalController"].ToString();

                var response = await this.apiService.Get<T_postal>(urlApi, prefix, controller, this.local.Id_Colonia);

                if (!response.IsSuccess)
                {
                    this.IsRefreshing = false;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                    return;
                }

                this.postal = (T_postal)response.Result;

                //-----------------Cargar Datos Ciudad-----------------//

                controller = Application.Current.Resources["UrlT_ciudadController"].ToString();

                response = await this.apiService.Get<T_ciudad>(urlApi, prefix, controller, this.local.Id_Ciudad);

                if (!response.IsSuccess)
                {
                    this.IsRefreshing = false;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                    return;
                }

                this.ciudad = (T_ciudad)response.Result;

                //-----------------Cargar Datos Estado-----------------//

                controller = Application.Current.Resources["UrlT_estadoController"].ToString();

                response = await this.apiService.Get<T_estado>(urlApi, prefix, controller, this.local.Id_Estado);

                if (!response.IsSuccess)
                {
                    this.IsRefreshing = false;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                    return;
                }

                this.estado = (T_estado)response.Result;

                address = $"{this.local.Calle} {this.local.Numero}, {this.postal.Asentamiento} {this.postal.Colonia}, C.P. {this.postal.Id.ToString()}, {this.ciudad.Ciudad}, {this.estado.Estado}.";
                this.empresaUser = MainViewModel.GetInstance().Login.ListUsuarios.Single(e => e.Id_usuario == this.empresa.Id_Usuario);
                this.IsRefreshing = false;
                this.IsRunning = false;
            }
        }
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
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.LocalTecnicoList = (List<T_tecnicos>)response.Result;
            this.IsRefreshing = false;
            this.IsRunning = false;
            this.RefreshTecnicoList();
        }
        public void RefreshTecnicoList()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            var tecnico = this.LocalTecnicoList.Select(t => new TecnicoItemViewModel
            {
                Apellido1 = t.Apellido1,
                Apellido2 = t.Apellido2,
                Apodo = t.Apodo,
                Carrera = t.Carrera,
                Id_Empresa = t.Id_Empresa,
                Id_Local = t.Id_Local,
                Id_Tecnico = t.Id_Tecnico,
                Id_Usuario = t.Id_Usuario,
                Nombre = t.Nombre,
            }).Where(t => t.Id_Local == this.local.Id_Local && userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)).ToList();
            this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
        }
        #endregion
    }
}
