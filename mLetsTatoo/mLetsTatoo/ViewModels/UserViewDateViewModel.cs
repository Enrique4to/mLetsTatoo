namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class UserViewDateViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string address;
        private string studio;
        private string reference;
        private string subTotal;
        private string advance;
        private string total;

        private DateTime appointmentDate;
        private TimeSpan appointmentTime;

        private T_trabajocitas cita;
        private T_usuarios user;
        private T_clientes cliente;
        private T_trabajos trabajo;
        private T_tecnicos tecnico;
        private T_locales local;
        private T_empresas empresa;
        private T_estado estado;
        private T_ciudad ciudad;
        private T_postal postal;
        public T_citaimagenes image;

        private ObservableCollection<NotasItemViewModel> notas;
        #endregion

        #region Properties
        public string Address
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }
        public string Studio
        {
            get { return this.studio; }
            set { SetValue(ref this.studio, value); }
        }
        public string Reference
        {
            get { return this.reference; }
            set { SetValue(ref this.reference, value); }
        }
        public string SubTotal
        {
            get { return this.subTotal; }
            set { SetValue(ref this.subTotal, value); }
        }
        public string Advance
        {
            get { return this.advance; }
            set { SetValue(ref this.advance, value); }
        }
        public string Total
        {
            get { return this.total; }
            set { SetValue(ref this.total, value); }
        }

        public DateTime MinDate { get; set; }
        public DateTime AppointmentDate
        {
            get { return this.appointmentDate; }
            set { SetValue(ref this.appointmentDate, value); }
        }
        public TimeSpan AppointmentTime
        {
            get { return this.appointmentTime; }
            set
            {
                SetValue(ref this.appointmentTime, value);
            }
        }

        public T_trabajocitas Cita
        {
            get { return this.cita; }
            set { SetValue(ref this.cita, value); }
        }
        public T_citaimagenes Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }

        public ObservableCollection<NotasItemViewModel> Notas
        {
            get { return this.notas; }
            set { this.notas = value; }
        }
        #endregion

        #region Constructors
        public UserViewDateViewModel(T_trabajocitas cita, T_usuarios user, T_clientes cliente)
        {
            this.cita = cita;
            this.user = user;
            this.cliente = cliente;
            this.apiService = new ApiService();
            this.AppointmentDate = this.cita.F_Inicio;
            this.AppointmentTime = this.cita.H_Inicio;
            this.MinDate = DateTime.Now.ToLocalTime();
            Task.Run(async () => { await this.LoadInfo(); }).Wait();
        }
        #endregion
        #region Commands

        #endregion
        #region Methods
        public async Task LoadInfo()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }
            //------------------------Cargar Datos de Trabajo ------------------------//
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Get<T_trabajos>(urlApi, prefix, controller, this.cita.Id_Trabajo);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.trabajo = (T_trabajos)response.Result;

            this.subTotal = $"{Languages.SubTotal} {this.trabajo.Total_Aprox.ToString("C2")}";
            this.advance = $"{Languages.Advance} {this.trabajo.Costo_Cita.ToString("C2")}";
            var tot = this.trabajo.Total_Aprox - this.trabajo.Costo_Cita;
            this.total = $"{Languages.Remaining} {tot.ToString("C2")}";

            //------------------------Cargar Datos de Tecnico ------------------------//

            controller = Application.Current.Resources["UrlT_tecnicosController"].ToString();

            response = await this.apiService.Get<T_tecnicos>(urlApi, prefix, controller, this.trabajo.Id_Tatuador);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.tecnico = (T_tecnicos)response.Result;

            //------------------------Cargar Datos de Local ------------------------//

            controller = App.Current.Resources["UrlT_localesController"].ToString();

            response = await this.apiService.Get<T_locales>(urlApi, prefix, controller, this.tecnico.Id_Local);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.local = (T_locales)response.Result;
            this.reference = $"{Languages.Reference} {this.local.Referencia}";

            //------------------------Cargar Datos de Empresa ------------------------//
            controller = App.Current.Resources["UrlT_empresasController"].ToString();

            response = await this.apiService.Get<T_empresas>(urlApi, prefix, controller, this.local.Id_Empresa);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.empresa = (T_empresas)response.Result;
            this.studio = $"{this.empresa.Nombre} {Languages.BranchOffice} {this.local.Nombre}";

            //-----------------Cargar Datos Postal-----------------//

            controller = Application.Current.Resources["UrlT_postalController"].ToString();

            response = await this.apiService.Get<T_postal>(urlApi, prefix, controller, this.local.Id_Colonia);

            if (!response.IsSuccess)
            {
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
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.estado = (T_estado)response.Result;

            this.address = $"{this.local.Calle} {this.local.Numero}, {this.postal.Asentamiento} {this.postal.Colonia}, C.P. {this.postal.Id.ToString()}, {this.ciudad.Ciudad}, {this.estado.Estado}.";
            
            //-----------------Cargar Datos Imagenes-----------------//

            controller = Application.Current.Resources["UrlT_citaimagenesController"].ToString();

            response = await this.apiService.Get<T_citaimagenes>(urlApi, prefix, controller, this.trabajo.Id_Trabajo);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.image = (T_citaimagenes)response.Result;
            //-----------------Cargar Notas-----------------//

            controller = App.Current.Resources["UrlT_trabajonotaController"].ToString();

            response = await this.apiService.GetList<T_trabajonota>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var notaList = (List<T_trabajonota>)response.Result;
            notaList = notaList.Where(c => c.Id_De == this.cliente.Id_Cliente).ToList();

            var nota = notaList.Select(c => new NotasItemViewModel
            {
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,
                Id_De = c.Id_De,
                Tipo_Usuario = c.Tipo_Usuario,
                F_nota = c.F_nota,
                Id_Local = c.Id_Local,
                Id_Nota = c.Id_Nota,
                Nota = c.Nota,
                Nombre_Post = c.Nombre_Post,
                Imagen_Post = c.Imagen_Post,

            }).Where(c => c.Id_Cita == this.cita.Id_Cita).ToList();
            this.Notas = new ObservableCollection<NotasItemViewModel>(nota.OrderBy(c => c.F_nota));



        }

        #endregion

    }
}
