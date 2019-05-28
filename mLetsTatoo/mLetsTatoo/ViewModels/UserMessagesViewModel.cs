namespace mLetsTatoo.ViewModels
{
    using Helpers;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class UserMessagesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<TrabajosTempItemViewModel> trabajos;

        public ClientesCollection cliente;
        public T_usuarios user;
        #endregion

        #region Properties
        public List<T_trabajostemp> TrabajosList { get; set; }
        public List<T_trabajonotatemp> TrabajoNotaList { get; set; }
        public List<TrabajosTempItemViewModel> TrabajoTempList { get; set; }

        public List<T_citaimagenestemp> ImagesList { get; set; }

        public ObservableCollection<TrabajosTempItemViewModel> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
        }
        #endregion

        #region Constructors
        public UserMessagesViewModel(T_usuarios user, ClientesCollection cliente)
        {
            this.user = user;
            this.cliente = cliente;
            this.apiService = new ApiService();
            this.RefreshTrabajosList();
        }
        #endregion

        #region Commands

        #endregion

        #region Methods
        public void RefreshTrabajosList()
        {
            this.TrabajoTempList = MainViewModel.GetInstance().Login.TrabajosTempList.Select(t => new TrabajosTempItemViewModel
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

                Nota = MainViewModel.GetInstance().Login.TrabajoNotaTempList.LastOrDefault(n => n.Id_Trabajotemp == t.Id_Trabajotemp).Nota,
                F_nota = MainViewModel.GetInstance().Login.TrabajoNotaTempList.LastOrDefault(n => n.Id_Trabajotemp == t.Id_Trabajotemp).F_nota,

                Nombre = MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Tecnico == t.Id_Tatuador).Nombre,
                Apellido = MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault( c => c.Id_Tecnico == t.Id_Tatuador).Apellido,

                F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.FirstOrDefault(
                    u => u.Id_usuario == MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(
                    c => c.Id_Tecnico == t.Id_Tatuador).Id_Usuario).F_Perfil,

                Imagen = MainViewModel.GetInstance().Login.ImagesTempList.FirstOrDefault(i => i.Id_Trabajotemp == t.Id_Trabajotemp).Imagen,

            }).Where(t => t.Id_Cliente == this.cliente.Id_Cliente).ToList();

            this.Trabajos = new ObservableCollection<TrabajosTempItemViewModel>(this.TrabajoTempList.OrderByDescending(t => t.F_nota));
        }

        #endregion
    }
}
