namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class LocalItemViewModel : T_locales
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public T_clientes cliente;
        public T_usuarios user;
        #endregion

        #region Constructors
        public LocalItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        //#region Commands
        //public ICommand EmpresaPageCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(GoToEmpresaPage);
        //    }
        //}
        //#endregion

        //#region Methods
        //private async void GoToEmpresaPage()
        //{
        //    user = MainViewModel.GetInstance().UserHome.User;
        //    cliente = MainViewModel.GetInstance().UserHome.Cliente;
        //    this.NombreEmpresa = this.Nombre;
        //    MainViewModel.GetInstance().Empresa = new EmpresaViewModel(this, user, cliente);
        //    await Application.Current.MainPage.Navigation.PushAsync(new EmpresaPage());
        //}
        //#endregion
    }
}
