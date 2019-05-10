namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.CustomPages;
    using mLetsTatoo.Views;
    using Models;
    using Popups.ViewModel;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Xamarin.Forms;

    public class TecnicoEditFeaturesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private INavigation Navigation;

        private T_usuarios user;
        private TecnicosCollection tecnico;

        private T_teccaract fSE;
        private T_teccaract fSM;
        private T_teccaract fSH;

        private T_teccaract fME;
        private T_teccaract fMM;
        private T_teccaract fMH;

        private T_teccaract fBE;
        private T_teccaract fBM;
        private T_teccaract fBH;

        private bool isEnabled;
        private bool isRunning;

        #endregion

        #region Properties
        public List<T_teccaract> FeaturesList { get; set; }

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


        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool IsRunning
        { 
                get { return this.isRunning; }
                set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructors
        public TecnicoEditFeaturesViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.tecnico = tecnico;
            this.user = user;
            this.apiService = new ApiService();
            this.LoadFeatures();
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(SaveFeatures);
            }
        }
        #endregion

        #region Merhods

        private async void SaveFeatures()
        {
            MainViewModel.GetInstance().ActivityIndicatorPopup = new ActivityIndicatorPopupViewModel();
            MainViewModel.GetInstance().ActivityIndicatorPopup.IsRunning = true;
            await Navigation.PushPopupAsync(new ActivityIndicatorPopupPage());

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            #region Small

            #region Save SmallEasy

            this.fSE = new T_teccaract
            {
                Id_Tecnico = this.FSE.Id_Tecnico,
                Id_Caract = this.FSE.Id_Caract,
                Alto = this.FSE.Alto,
                Ancho= this.FSE.Ancho,
                Caract = this.FSE.Caract,
                Costo_Cita = this.FSE.Costo_Cita,
                Imagen_Ejemplo = this.FSE.Imagen_Ejemplo,
                Tiempo = this.FSE.Tiempo,
                Total_Aprox = this.FSE.Total_Aprox,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            var response = await this.apiService.Put(urlApi, prefix, controller, this.fSE, this.FSE.Id_Caract);

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

            var oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.fSE.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #region Save SmallMedium
            this.fSM = new T_teccaract
            {
                Id_Tecnico = this.FSM.Id_Tecnico,
                Id_Caract = this.FSM.Id_Caract,
                Alto = this.FSM.Alto,
                Ancho = this.FSM.Ancho,
                Caract = this.FSM.Caract,
                Costo_Cita = this.FSM.Costo_Cita,
                Imagen_Ejemplo = this.FSM.Imagen_Ejemplo,
                Tiempo = this.FSM.Tiempo,
                Total_Aprox = this.FSM.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fSM, this.FSM.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.fSM.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #region Save SmallHard
            this.fSH = new T_teccaract
            {
                Id_Tecnico = this.FSH.Id_Tecnico,
                Id_Caract = this.FSH.Id_Caract,
                Alto = this.FSH.Alto,
                Ancho = this.FSH.Ancho,
                Caract = this.FSH.Caract,
                Costo_Cita = this.FSH.Costo_Cita,
                Imagen_Ejemplo = this.FSH.Imagen_Ejemplo,
                Tiempo = this.FSH.Tiempo,
                Total_Aprox = this.FSH.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fSH, this.FSH.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FSH.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #endregion

            #region MediumSize
            #region Save MediumEasy
            this.fME = new T_teccaract
            {
                Id_Tecnico = this.FME.Id_Tecnico,
                Id_Caract = this.FME.Id_Caract,
                Alto = this.FME.Alto,
                Ancho = this.FME.Ancho,
                Caract = this.FME.Caract,
                Costo_Cita = this.FME.Costo_Cita,
                Imagen_Ejemplo = this.FME.Imagen_Ejemplo,
                Tiempo = this.FME.Tiempo,
                Total_Aprox = this.FME.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fME, this.FME.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FME.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #region Save MediumMedium
            this.fMM = new T_teccaract
            {
                Id_Tecnico = this.FMM.Id_Tecnico,
                Id_Caract = this.FMM.Id_Caract,
                Alto = this.FMM.Alto,
                Ancho = this.FMM.Ancho,
                Caract = this.FMM.Caract,
                Costo_Cita = this.FMM.Costo_Cita,
                Imagen_Ejemplo = this.FMM.Imagen_Ejemplo,
                Tiempo = this.FMM.Tiempo,
                Total_Aprox = this.FMM.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fMM, this.FMM.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FMM.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #region Save MediumHard
            this.fMH = new T_teccaract
            {
                Id_Tecnico = this.FMH.Id_Tecnico,
                Id_Caract = this.FMH.Id_Caract,
                Alto = this.FMH.Alto,
                Ancho = this.FMH.Ancho,
                Caract = this.FMH.Caract,
                Costo_Cita = this.FMH.Costo_Cita,
                Imagen_Ejemplo = this.FMH.Imagen_Ejemplo,
                Tiempo = this.FMH.Tiempo,
                Total_Aprox = this.FMH.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fMH, this.FMH.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FMH.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #endregion

            #region BigSize

            #region Save BigEasy
            this.fBE = new T_teccaract
            {
                Id_Tecnico = this.FBE.Id_Tecnico,
                Id_Caract = this.FBE.Id_Caract,
                Alto = this.FBE.Alto,
                Ancho = this.FBE.Ancho,
                Caract = this.FBE.Caract,
                Costo_Cita = this.FBE.Costo_Cita,
                Imagen_Ejemplo = this.FBE.Imagen_Ejemplo,
                Tiempo = this.FBE.Tiempo,
                Total_Aprox = this.FBE.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fBE, this.FBE.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FBE.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #region Save BigMedium
            this.fBM = new T_teccaract
            {
                Id_Tecnico = this.FBM.Id_Tecnico,
                Id_Caract = this.FBM.Id_Caract,
                Alto = this.FBM.Alto,
                Ancho = this.FBM.Ancho,
                Caract = this.FBM.Caract,
                Costo_Cita = this.FBM.Costo_Cita,
                Imagen_Ejemplo = this.FBM.Imagen_Ejemplo,
                Tiempo = this.FBM.Tiempo,
                Total_Aprox = this.FBM.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fBM, this.FBM.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FBM.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion

            #region Save BigHard
            this.fBH = new T_teccaract
            {
                Id_Tecnico = this.FBH.Id_Tecnico,
                Id_Caract = this.FBH.Id_Caract,
                Alto = this.FBH.Alto,
                Ancho = this.FBH.Ancho,
                Caract = this.FBH.Caract,
                Costo_Cita = this.FBH.Costo_Cita,
                Imagen_Ejemplo = this.FBH.Imagen_Ejemplo,
                Tiempo = this.FBH.Tiempo,
                Total_Aprox = this.FBH.Total_Aprox,
            };

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, this.fBH, this.FBH.Id_Caract);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            NewCaract = (T_teccaract)response.Result;

            oldCaract = this.FeaturesList.Where(c => c.Id_Caract == this.FBH.Id_Caract).FirstOrDefault();
            if (oldCaract != null)
            {
                this.FeaturesList.Remove(oldCaract);
            }
            this.FeaturesList.Add(NewCaract);
            #endregion
            #endregion

            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PopPopupAsync();
        }

        public async void LoadFeatures()
        {
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            var response = await this.apiService.GetList<T_teccaract>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.FeaturesList = (List<T_teccaract>)response.Result;

            this.FeaturesList = this.FeaturesList.Where(f => f.Id_Tecnico == this.tecnico.Id_Tecnico).ToList();

            this.FSE = this.FeaturesList.Single(f => f.Caract == "SmallEasy");
            this.FSM = this.FeaturesList.Single(f => f.Caract == "SmallMedium");
            this.FSH = this.FeaturesList.Single(f => f.Caract == "SmallHard");
            this.FME = this.FeaturesList.Single(f => f.Caract == "MediumEasy");
            this.FMM = this.FeaturesList.Single(f => f.Caract == "MediumMedium");
            this.FMH = this.FeaturesList.Single(f => f.Caract == "MediumHard");
            this.FBE = this.FeaturesList.Single(f => f.Caract == "BigEasy");
            this.FBM = this.FeaturesList.Single(f => f.Caract == "BigMedium");
            this.FBH = this.FeaturesList.Single(f => f.Caract == "BigHard");


            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoEditFeaturesPage());

            var daStack = Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count();
            if (daStack > 0)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }

        }
        #endregion
    }
}
