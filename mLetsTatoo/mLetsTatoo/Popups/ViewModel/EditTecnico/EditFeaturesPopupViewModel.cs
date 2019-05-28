namespace mLetsTatoo.Popups.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.CustomPages;
    using mLetsTatoo.Views;
    using Models;
    using Plugin.Media.Abstractions;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using ViewModels;
    using Xamarin.Forms;

    public class EditFeaturesPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        private T_teccaract fSE;
        private T_teccaract fSM;
        private T_teccaract fSH;

        private T_teccaract fME;
        private T_teccaract fMM;
        private T_teccaract fMH;

        private T_teccaract fBE;
        private T_teccaract fBM;
        private T_teccaract fBH;

        private bool next;
        public string page;

        #endregion

        #region Properties
        public T_teccaract FSE
        {
            get { return this.fSE; }
            set { SetValue(ref this.fSE, value); }
        }
        public T_teccaract FSM
        {
            get { return this.fSM; }
            set { SetValue(ref this.fSM, value); }
        }
        public T_teccaract FSH
        {
            get { return this.fSH; }
            set { SetValue(ref this.fSH, value); }
        }

        public T_teccaract FME
        {
            get { return this.fME; }
            set { SetValue(ref this.fME, value); }
        }
        public T_teccaract FMM
        {
            get { return this.fMM; }
            set { SetValue(ref this.fMM, value); }
        }
        public T_teccaract FMH
        {
            get { return this.fMH; }
            set { SetValue(ref this.fMH, value); }
        }

        public T_teccaract FBE
        {
            get { return this.fBE; }
            set { SetValue(ref this.fBE, value); }
        }
        public T_teccaract FBM
        {
            get { return this.fBM; }
            set { SetValue(ref this.fBM, value); }
        }
        public T_teccaract FBH
        {
            get { return this.fBH; }
            set { SetValue(ref this.fBH, value); }
        }

        #endregion

        #region Constructors
        public EditFeaturesPopupViewModel()
        {
            this.apiService = new ApiService();
            this.LoadFeatures();
        }
        #endregion

        #region Commands

        public ICommand NextCommand
        {
            get
            {
                return new RelayCommand(GoToNextPopupPage);
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }
        #endregion

        #region Methods
        public void LoadFeatures()
        {
            this.FSE = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "SmallEasy");
            this.FSM = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "SmallMedium");
            this.FSH = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "SmallHard");
            this.FME = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "MediumEasy");
            this.FMM = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "MediumMedium");
            this.FMH = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "MediumHard");
            this.FBE = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "BigEasy");
            this.FBM = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "BigMedium");
            this.FBH = MainViewModel.GetInstance().TecnicoHome.ListFeature.FirstOrDefault(f => f.Caract == "BigHard");
        }

        private void ValidateSmall()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.FSE.Alto.ToString()) || string.IsNullOrEmpty(this.FSE.Ancho.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FSE.Alto < 0 || this.FSE.Ancho < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.FSE.Total_Aprox.ToString())
                || string.IsNullOrEmpty(this.FSM.Total_Aprox.ToString())
                || string.IsNullOrEmpty(this.FSH.Total_Aprox.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FSE.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FSM.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FSH.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.FSE.Costo_Cita.ToString())
                || string.IsNullOrEmpty(this.FSM.Costo_Cita.ToString())
                || string.IsNullOrEmpty(this.FSH.Costo_Cita.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FSE.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FSM.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FSH.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FSE.Tiempo <= 0 || this.FSM.Tiempo <= 0 || this.FSH.Tiempo <= 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                this.next = false;
                return;
            }
        }
        private void ValidateMediumSize()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.FME.Alto.ToString()) || string.IsNullOrEmpty(this.FME.Ancho.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FME.Alto < 0 || this.FME.Ancho < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.FME.Total_Aprox.ToString())
                || string.IsNullOrEmpty(this.FMM.Total_Aprox.ToString())
                || string.IsNullOrEmpty(this.FMH.Total_Aprox.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FME.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FMM.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FMH.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.FME.Costo_Cita.ToString())
                || string.IsNullOrEmpty(this.FMM.Costo_Cita.ToString())
                || string.IsNullOrEmpty(this.FMH.Costo_Cita.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FME.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FMM.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FMH.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FME.Tiempo <= 0 || this.FMM.Tiempo <= 0 || this.FMH.Tiempo <= 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                this.next = false;
                return;
            }
        }
        private void ValidateBig()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.FBE.Alto.ToString()) || string.IsNullOrEmpty(this.FBE.Ancho.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FBE.Alto < 0 || this.FBE.Ancho < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.FBE.Total_Aprox.ToString())
                || string.IsNullOrEmpty(this.FBM.Total_Aprox.ToString())
                || string.IsNullOrEmpty(this.FBH.Total_Aprox.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FBE.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FBM.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FBH.Total_Aprox < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.FBE.Costo_Cita.ToString())
                || string.IsNullOrEmpty(this.FBM.Costo_Cita.ToString())
                || string.IsNullOrEmpty(this.FBH.Costo_Cita.ToString()))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.FBE.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FBM.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FBH.Costo_Cita < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.FBE.Tiempo <= 0 || this.FBM.Tiempo <= 0 || this.FBH.Tiempo <= 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                this.next = false;
                return;
            }
        }
        private async void SaveData()
        {
            var list = new List<T_teccaract>();
            list.Add(this.FSE);
            list.Add(this.FSM);
            list.Add(this.FSH);
            list.Add(this.FME);
            list.Add(this.FMM);
            list.Add(this.FMH);
            list.Add(this.FBE);
            list.Add(this.FBM);
            list.Add(this.FBH);

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "Ok");
                return;
            }
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            foreach (var feature in list)
            {
                var response = await this.apiService.Put(urlApi, prefix, controller, feature, feature.Id_Caract);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.Navigation.PopPopupAsync();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }
                var NewCaract = (T_teccaract)response.Result;

                var oldCaract = MainViewModel.GetInstance().TecnicoHome.ListFeature.Where(c => c.Id_Caract == feature.Id_Caract).FirstOrDefault();
                if (oldCaract != null)
                {
                    MainViewModel.GetInstance().TecnicoHome.ListFeature.Remove(oldCaract);
                }
                MainViewModel.GetInstance().TecnicoHome.ListFeature.Add(NewCaract);
            }
        }

        private async void GoToNextPopupPage()
        {
            switch (this.page)
            {
                case "Small":
                    this.ValidateSmall();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Medium";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditMediumPopupPage());
                    break;

                case "Medium":
                    this.ValidateMediumSize();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Big";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditBigPopupPage());
                    break;

                case "Big":
                    this.ValidateBig();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();

                    this.SaveData();

                    await Application.Current.MainPage.DisplayAlert(Languages.Notice, "Done", "Ok");

                    break;
            }
        }
        private async void Cancel()
        {
            switch (this.page)
            {
                case "Small":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    break;

                case "Medium":
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditSmallPopupPage());
                    break;

                case "Big":
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditMediumPopupPage());
                    break;
            }
        }
        #endregion

    }
}
