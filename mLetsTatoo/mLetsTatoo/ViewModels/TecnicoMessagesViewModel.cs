namespace mLetsTatoo.ViewModels
{
    using Helpers;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class TecnicoMessagesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<TrabajosTempItemViewModel> trabajos;

        public TecnicosCollection tecnico;
        public T_usuarios user;
        #endregion

        #region Properties
        public List<T_clientes> ListClientes { get; set; }
        public List<T_trabajostemp> TrabajosList { get; set; }
        public List<T_trabajonotatemp> TrabajoNotaList { get; set; }

        public List<T_citaimagenestemp> ImagesList { get; set; }

        public ObservableCollection<TrabajosTempItemViewModel> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
        }
        #endregion

        #region Constructors
        public TecnicoMessagesViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.LoadTrabajoNotas();
        }
        #endregion

        #region Commands

        #endregion

        #region Methods
        private async void LoadTrabajoNotas()
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

            var response = await this.apiService.GetList<T_trabajostemp>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.TrabajosList = (List<T_trabajostemp>)response.Result;

            controller = Application.Current.Resources["UrlT_trabajonotatempController"].ToString();

            response = await this.apiService.GetList<T_trabajonotatemp>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.TrabajoNotaList = (List<T_trabajonotatemp>)response.Result;

            controller = Application.Current.Resources["UrlT_citaimagenestempController"].ToString();

            response = await this.apiService.GetList<T_citaimagenestemp>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ImagesList = (List<T_citaimagenestemp>)response.Result;

            this.RefreshTrabajosList();

            this.apiService.EndActivityPopup();
        }
        public void RefreshTrabajosList()
        {
            var trabajoTemp = this.TrabajosList.Select(t => new TrabajosTempItemViewModel
            {
                Id_Trabajotemp = t.Id_Trabajotemp,
                Id_Tatuador = t.Id_Tatuador,
                Id_Cliente= t.Id_Cliente,
                Asunto = t.Asunto,
                Costo_Cita = t.Costo_Cita,
                Total_Aprox = t.Total_Aprox,
                Alto = t.Alto,
                Ancho = t.Ancho,
                Tiempo = t.Tiempo,

                Nota = this.TrabajoNotaList.LastOrDefault(n => n.Id_Trabajotemp == t.Id_Trabajotemp).Nota,
                F_nota = this.TrabajoNotaList.LastOrDefault(n => n.Id_Trabajotemp == t.Id_Trabajotemp).F_nota,

                Nombre = MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault(c => c.Id_Cliente == t.Id_Cliente).Nombre,
                Apellido = MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault( c => c.Id_Cliente == t.Id_Cliente).Apellido,

                F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.FirstOrDefault(
                    u => u.Id_usuario == MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault(
                    c => c.Id_Cliente == t.Id_Cliente).Id_Usuario).F_Perfil,

                Imagen = this.ImagesList.First(i => i.Id_Trabajotemp == t.Id_Trabajotemp).Imagen,

            }).Where(t => t.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();

            this.Trabajos = new ObservableCollection<TrabajosTempItemViewModel>(trabajoTemp.OrderByDescending(t => t.F_nota));
        }

        #endregion
    }
}
