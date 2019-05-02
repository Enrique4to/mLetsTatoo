namespace mLetsTatoo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Models;
    using mLetsTatoo.Services;
    using mLetsTatoo.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NotasTempItemViewModel : TrabajonotaTempCollection
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private TecnicosCollection tecnico;
        private T_usuarios user;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public NotasTempItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands

        #endregion

        #region Methods
        #endregion
    }
}
