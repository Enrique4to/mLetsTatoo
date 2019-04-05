using GalaSoft.MvvmLight.Command;
using mLetsTatoo.Helpers;
using mLetsTatoo.Models;
using mLetsTatoo.Services;
using mLetsTatoo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mLetsTatoo.ViewModels
{
    public class CitasViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;
        public string filter;
        private string selectedArtist;
        public string busqueda;
        public T_tecnicos tecnico;
        #endregion

        #region Properties
        
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
        public string Filter
        {
            get { return this.filter; }
            set { SetValue(ref this.filter, value); }
        }
        public string SelectedArtist
        {
            get { return this.selectedArtist; }
            set { SetValue(ref this.selectedArtist, value); }
        }
        #endregion

        #region Constructors
        public CitasViewModel(T_tecnicos tecnico)
        {
            this.tecnico = tecnico;
            this.apiService = new ApiService();

            if (MainViewModel.GetInstance().Tecnico == null)
            {
                this.selectedArtist = Languages.SelectArtist;
            }
            else
            {
                this.selectedArtist = MainViewModel.GetInstance().Tecnico.SelectedArtist;
            }
            if(this.tecnico != null)
            {
                this.selectedArtist = $"{this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido1}";
            }
        }
        #endregion

        #region Commands
        public ICommand SearchArtistCommand
        {
            get
            {
                return new RelayCommand(GoToSearch);
            }
        }
        #endregion

        #region Methods
        private async void GoToSearch()
        {
            this.busqueda = "SI";
            MainViewModel.GetInstance().Search = new SearchViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchPage());
        }
        #endregion

    }
}
