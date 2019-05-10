namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Services;
    using Models;
    using Helpers;
    using Xamarin.Forms;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Extensions;
    using mLetsTatoo.Popups.Views;

    public class NotasItemViewModel : TrabajoNotaCollection

    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        #endregion

        #region Properties
        #endregion

        #region Constructors
        public NotasItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand SelectedNotaCommand
        {
            get
            {
                return new RelayCommand(SelectedNota);
            }
        }
        #endregion

        #region Methods
        private async void SelectedNota()
        {
            if (MainViewModel.GetInstance().UserViewDate != null)
            { 
                if (MainViewModel.GetInstance().UserViewDate.user.Tipo == this.Tipo_Usuario)
                {
                    MainViewModel.GetInstance().UserViewDate.IsButtonEnabled = true;
                    MainViewModel.GetInstance().UserViewDate.IsVisible = true;
                    MainViewModel.GetInstance().UserViewDate.notaSelected = this;
                }
                else
                {
                    if (this.Cambio_Fecha == true)
                    {
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ChangeDatePopupPage());
                        return;
                    }
                    MainViewModel.GetInstance().UserViewDate.IsButtonEnabled = false;
                    MainViewModel.GetInstance().UserViewDate.IsVisible = false;
                    MainViewModel.GetInstance().UserViewDate.notaSelected = null;
                }
            }

            if (MainViewModel.GetInstance().TecnicoViewDate != null)
            {
                if (MainViewModel.GetInstance().TecnicoViewDate.user.Tipo == this.Tipo_Usuario)
                {
                    MainViewModel.GetInstance().TecnicoViewDate.IsButtonEnabled = true;
                    MainViewModel.GetInstance().TecnicoViewDate.IsVisible = true;
                    MainViewModel.GetInstance().TecnicoViewDate.notaSelected = this;
                }
                else
                {
                    MainViewModel.GetInstance().TecnicoViewDate.IsButtonEnabled = false;
                    MainViewModel.GetInstance().TecnicoViewDate.IsVisible = false;
                    MainViewModel.GetInstance().TecnicoViewDate.notaSelected = null;
                }
            }
        }
        #endregion
    }
}
