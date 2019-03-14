using System;
using System.Collections.Generic;
using System.Text;

namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using ViewModels;
    using Services;
    using Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class HomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion
        #region Attributes
        private ObservableCollection<Tusuarios> usuarios;
        private bool isRefreshing;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public ObservableCollection<Tusuarios> Usuarios
        {
            get { return this.usuarios; }
            set { SetValue(ref this.usuarios, value); }
        }
        #endregion
        #region Constructors
        public HomeViewModel()
        {
            this.apiService = new ApiService();
            this.LoadUsuarios();
        }
        #endregion
        #region Methods
        private async void LoadUsuarios()
        {

            this.IsRefreshing = false;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetList<Tusuarios>(
                url,
                "/api",
                "/Tusuarios");
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "OK");
                return;
            }
            var list = (List<Tusuarios>)response.Result;
            this.Usuarios = new ObservableCollection<Tusuarios>(list);
        }
        #endregion
        #region Commans
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadUsuarios);
            }
        }
        #endregion
    }
}
