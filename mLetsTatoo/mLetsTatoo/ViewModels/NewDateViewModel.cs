using GalaSoft.MvvmLight.Command;
using mLetsTatoo.Helpers;
using mLetsTatoo.Models;
using mLetsTatoo.Services;
using mLetsTatoo.Views;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mLetsTatoo.ViewModels
{
    public class NewDateViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;
        public string filter;
        private string selectedArtist;
        private string describeArt;
        public T_tecnicos tecnico;
        public T_usuarios user;
        public T_clientes cliente;

        private bool smallChecked;
        private bool mediumSizeChecked;
        private bool bigChecked;
        private bool easyChecked;
        private bool mediumComplexityChecked;
        private bool hardChecked;
        #endregion

        #region Properties
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool SmallChecked
        {
            get { return this.smallChecked; }
            set { SetValue(ref this.smallChecked, value); }
        }
        public bool MediumSizeChecked
        {
            get { return this.mediumSizeChecked; }
            set { SetValue(ref this.mediumSizeChecked, value); }
        }
        public bool BigChecked
        {
            get { return this.bigChecked; }
            set { SetValue(ref this.bigChecked, value); }
        }
        public bool EasyChecked
        {
            get { return this.easyChecked; }
            set { SetValue(ref this.easyChecked, value); }
        }
        public bool MediumComplexityChecked
        {
            get { return this.mediumComplexityChecked; }
            set { SetValue(ref this.mediumComplexityChecked, value); }
        }
        public bool HardChecked
        {
            get { return this.hardChecked; }
            set { SetValue(ref this.hardChecked, value); }
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
        public string DescribeArt
        {
            get { return this.describeArt; }
            set { SetValue(ref this.describeArt, value); }
        }
        #endregion

        #region Constructors
        public NewDateViewModel(T_tecnicos tecnico, T_usuarios user, T_clientes cliente)
        {
            this.tecnico = tecnico;
            this.user = user;
            this.cliente = cliente;

            this.apiService = new ApiService();

            this.smallChecked = true;
            this.easyChecked = true;


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
                this.selectedArtist = $"Artista: {this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido1}";
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
        public ICommand SaveDateCommand
        {
            get
            {
                return new RelayCommand(SaveDate);
            }
        }
        #endregion

        #region Methods
        private async void GoToSearch()
        {
            MainViewModel.GetInstance().Search = new SearchViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchPage());
        }
        private async void SaveDate()
        {
            if(string.IsNullOrEmpty(this.selectedArtist))
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.SelectedArtistError,
                "Ok");
                return;
            }
            if (this.AppointmentDate < DateTime.Today)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.DateError,
                "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.describeArt))
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.DescribeArtError,
                "Ok");
                return;
            }

        }
        #endregion

    }
}
