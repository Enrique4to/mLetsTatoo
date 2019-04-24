namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class EmpresaItemViewModel : T_empresas
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public T_clientes cliente;
        public T_usuarios user;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public EmpresaItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EmpresaPageCommand
        {
            get
            {
                return new RelayCommand(GoToEmpresaPage);
            }
        }
        #endregion

        #region Methods
        private async void GoToEmpresaPage()
        {
            this.user = MainViewModel.GetInstance().UserHome.User;
            this.cliente = MainViewModel.GetInstance().UserHome.Cliente;
            MainViewModel.GetInstance().Empresa = new EmpresaViewModel(this, user, cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new EmpresaPage());
        }
        #endregion
    }
}
