namespace mLetsTatoo.ViewModels
{
    using Helpers;
    using Views;
    using Models;
    using Services;
    using System;
    using Xamarin.Forms;
    using System.IO;
    using System.Threading.Tasks;

    public class LocalViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private byte[] byteImage;
        private ImageSource imageSource;
        public T_locales local;
        public T_estado estado;
        public T_ciudad ciudad;
        public T_postal postal;
        public T_empresas empresa;
        private string address;
        private string nombreEmpresa;
        private string nombreLocal;
        #endregion

        #region Properties
        public string Address
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }
        public string NombreEmpresa
        {
            get { return this.nombreEmpresa; }
            set { SetValue(ref this.nombreEmpresa, value); }
        }
        public string NombreLocal
        {
            get { return this.nombreLocal; }
            set { SetValue(ref this.nombreLocal, value); }
        }
        public string DataCiudad{ get; set; }
        public string DataEstado { get; set; }
        public byte[] ByteImage
        {
            get { return this.byteImage; }
            set { SetValue(ref this.byteImage, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public T_postal Postal
        {
            get { return this.postal; }
            set { SetValue(ref this.postal, value); }
        }
        public T_ciudad Ciudad
        {
            get { return this.ciudad; }
            set { SetValue(ref this.ciudad, value); }
        }
        public T_estado Estado
        {
            get { return this.estado; }
            set { SetValue(ref this.estado, value); }
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
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructors
        public LocalViewModel(T_locales local, T_empresas empresa)
        {
            this.local = local;
            this.empresa = empresa;
            this.apiService = new ApiService();
            Task.Run(async () => { await LoadInfo(); }).Wait();
            this.LoadEmpresa();            
            this.IsRunning = false;
        }

        #endregion

        #region Commands

        #endregion

        #region Methods
        private void LoadEmpresa()
        {
            if (this.empresa.Logo != null)
            {
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.empresa.Logo));
            }
            else
            {
                this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }

            this.nombreEmpresa = this.empresa.Nombre;
            this.nombreLocal = this.local.Nombre;
        }
        public async Task LoadInfo()
        {

            if (string.IsNullOrEmpty(address))
            {
                this.IsRunning = true;
                var connection = await this.apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
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
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                    return;
                }

                this.estado = (T_estado)response.Result;

                address = $"{this.local.Calle} {this.local.Numero}, {this.postal.Asentamiento} {this.postal.Colonia}, C.P. {this.postal.Id.ToString()}, {this.ciudad.Ciudad}, {this.estado.Estado}.";

                this.IsRunning = false;
            }
        }
        #endregion
    }
}
