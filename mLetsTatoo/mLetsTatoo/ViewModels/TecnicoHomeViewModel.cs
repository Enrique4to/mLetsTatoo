namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Popups.ViewModel;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class TecnicoHomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;

        private ObservableCollection<TrabajosItemViewModel> trabajos;
        private ObservableCollection<PublicacionesItemViewModel> publicaciones;


        private T_clientes cliente;
        private T_usuarios user;
        public TecnicosCollection tecnico;
        private T_trabajos trabajo;

        private string file;
        public string filter;
        #endregion

        #region Properties
        public string TipoBusqueda { get; set; }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
            }
        }

        public List<T_trabajos> TrabajoList { get; set; }
        public List<T_trabajocitas> CitasList { get; set; }
        public List<T_tecnicohorarios> ListHorariosTecnicos { get; set; }
        public List<T_publicaciones> ListPublicaciones { get; set; }
        public List<T_imgpublicacion> ListImgPublicacion { get; set; }
        public List<T_comentpublicacion> ListComentPublicacion { get; set; }
        public List<PublicacionesItemViewModel> NewListPublicaciones { get; set; }

        public T_clientes Cliente
        {
            get { return this.cliente; }
            set { SetValue(ref this.cliente, value); }
        }
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public T_trabajos Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public ObservableCollection<TrabajosItemViewModel> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
        }
        public ObservableCollection<PublicacionesItemViewModel> Publicaciones
        {
            get { return this.publicaciones; }
            set { SetValue(ref this.publicaciones, value); }
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
        public TecnicoHomeViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.LoadTecnico();
            this.RefreshPublicaciones();
            this.LoadTrabajos();
            
            this.TipoBusqueda = "All";            
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Busqueda);
            }
        }
        public ICommand RefreshTrabajoCommand
        {
            get
            {
                return new RelayCommand(RefreshTrabajosList);
            }
        }
        public ICommand RefreshPublicacionesCommand
        {
            get
            {
                return new RelayCommand(RefreshPublicaciones);
            }
        }
        public ICommand NewPublicationCommand
        {
            get
            {
                return new RelayCommand(NewPublication);
            }
        }

        #endregion

        #region Methods

        public void LoadTecnico()
        {
            this.tecnico = new TecnicosCollection
            {
                Id_Usuario = this.user.Id_usuario,
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Nombre = this.tecnico.Nombre,
                Apellido = this.tecnico.Apellido,
                Apellido2 = this.tecnico.Apellido2,
                Apodo = this.tecnico.Apodo,
                Carrera = this.tecnico.Carrera,
                Id_Empresa = this.tecnico.Id_Empresa,
                Id_Local = this.tecnico.Id_Local,
                F_Perfil = this.tecnico.F_Perfil,
                Saldo_Favor =
                    (
                        !MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Usuario == this.user.Id_usuario) ?
                        0 :
                        MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Usuario == this.user.Id_usuario).Saldo_Favor
                    ),
                Saldo_Contra =
                    (
                        !MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Usuario == this.user.Id_usuario) ?
                        0 :
                        MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Usuario == this.user.Id_usuario).Saldo_Contra
                    ),
                Saldo_Retenido =
                    (
                        !MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Usuario == this.user.Id_usuario) ?
                        0 :
                        MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Usuario == this.user.Id_usuario).Saldo_Retenido
                    ),
            };
            this.IsRefreshing = true;

            MainViewModel.GetInstance().TecnicoProfile = new TecnicoProfileViewModel(this.user, this.tecnico);
            this.CitasList = MainViewModel.GetInstance().Login.CitaList;
            this.ListHorariosTecnicos = MainViewModel.GetInstance().Login.ListHorariosTecnicos;
            this.ListPublicaciones = MainViewModel.GetInstance().Login.ListPublicaciones;
            this.ListImgPublicacion = MainViewModel.GetInstance().Login.ListImgPublicacion;
            this.ListComentPublicacion = MainViewModel.GetInstance().Login.ListComentPublicacion;

            this.IsRefreshing = false;
        }
        private async void LoadTrabajos()
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
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.GetList<T_trabajos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.TrabajoList = (List<T_trabajos>)response.Result;
            this.RefreshTrabajosList();

            this.apiService.EndActivityPopup();
        }
        public void RefreshTrabajosList()
        {
            var trabajo = this.TrabajoList.Select(c => new TrabajosItemViewModel
            {

                Id_Trabajo = c.Id_Trabajo,
                Id_Cliente = c.Id_Cliente,
                Id_Tatuador = c.Id_Tatuador,
                Asunto = c.Asunto,
                Costo_Cita = c.Costo_Cita,
                Total_Aprox = c.Total_Aprox,
                Id_Caract = c.Id_Caract,
                Alto = c.Alto,
                Ancho = c.Ancho,
                Tiempo = c.Tiempo,
                Cancelado = c.Cancelado,
                Completo = c.Completo,
                Trabajo_Iniciado = c.Trabajo_Iniciado,
                TecnicoTiempo = c.TecnicoTiempo

            }).Where(c => c.Id_Tatuador == this.tecnico.Id_Tecnico && c.Cancelado == false && c.TecnicoTiempo == false).ToList();

            this.Trabajos = new ObservableCollection<TrabajosItemViewModel>(trabajo.OrderBy(c => c.Id_Trabajo));
            IsRefreshing = false;
        }
        public void RefreshPublicaciones()
        {
            this.NewListPublicaciones = this.ListPublicaciones.Select(p => new PublicacionesItemViewModel
            {
                Id_Publicacion = p.Id_Publicacion,
                Id_Usuario = p.Id_Usuario,
                Fecha_Publicacion = p.Fecha_Publicacion,
                Modif_Date = p.Modif_Date,
                Publicacion = p.Publicacion,
                F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.Where(u => u.Id_usuario == p.Id_Usuario).FirstOrDefault().F_Perfil,
                Tipo = MainViewModel.GetInstance().Login.ListUsuarios.Where(u => u.Id_usuario == p.Id_Usuario).FirstOrDefault().Tipo,

                Nombre =
                (
                    MainViewModel.GetInstance().Login.ListUsuarios.Where(u => u.Id_usuario == p.Id_Usuario).FirstOrDefault().Tipo == 1 ?
                    MainViewModel.GetInstance().Login.ClienteList.Where(c => c.Id_Usuario == p.Id_Usuario).FirstOrDefault().Nombre :
                    MainViewModel.GetInstance().Login.TecnicoList.Where(c => c.Id_Usuario == p.Id_Usuario).FirstOrDefault().Nombre
                ),
                Apellido =
                (
                    MainViewModel.GetInstance().Login.ListUsuarios.Where(u => u.Id_usuario == p.Id_Usuario).FirstOrDefault().Tipo == 1 ?
                    MainViewModel.GetInstance().Login.ClienteList.Where(c => c.Id_Usuario == p.Id_Usuario).FirstOrDefault().Apellido :
                    MainViewModel.GetInstance().Login.TecnicoList.Where(c => c.Id_Usuario == p.Id_Usuario).FirstOrDefault().Apellido
                ),

                ListImgPublicacion = this.ListImgPublicacion.Select(i => new T_imgpublicacion
                {
                    Id_Publicacion = i.Id_Publicacion,
                    Id_Usuario = i.Id_Usuario,
                    Id_Imgpublicacion = i.Id_Imgpublicacion,
                    Imagen = i.Imagen,

                }).Where(a => a.Id_Publicacion == p.Id_Publicacion).ToList(),

        }).ToList();
            this.NewListPublicaciones = this.NewListPublicaciones.Select(p => new PublicacionesItemViewModel
            {
                Id_Publicacion = p.Id_Publicacion,
                Id_Usuario = p.Id_Usuario,
                Fecha_Publicacion = p.Fecha_Publicacion,
                Modif_Date = p.Modif_Date,
                Publicacion = p.Publicacion,
                F_Perfil = p.F_Perfil,
                Tipo = p.Tipo,

                Nombre = p.Nombre,
                Apellido = p.Apellido,

                ListImgPublicacion = p.ListImgPublicacion,

                //OCImgPublicacion = new ObservableCollection<ImgPublicacionItemViewModel>(p.ListImgPublicacion),

            }).ToList();

            this.Publicaciones = new ObservableCollection<PublicacionesItemViewModel>(this.NewListPublicaciones.OrderBy(c => c.Modif_Date));
            IsRefreshing = false;
        }
        public void Busqueda()
        {
            if (TipoBusqueda == "All")
            {
            }
            if (TipoBusqueda == "Citas")
            {
            }
        }
        public async void GoToMessagesPage()
        {
            this.apiService.StartActivityPopup();

            MainViewModel.GetInstance().TecnicoMessages = new TecnicoMessagesViewModel(this.user, this.tecnico);
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoMessagesPage());

            //this.apiService.EndActivityPopup();
        }

        private async void NewPublication()
        {
            MainViewModel.GetInstance().NewPublicationPopup = new NewPublicationPopupViewModel();
            await Application.Current.MainPage.Navigation.PushPopupAsync(new NewPublicationPopupPage());
        }
        #endregion
    }
}
