namespace mLetsTatoo.Popups.ViewModel
{
    using System;
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

    public class ConfirmTecnicoPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private TecnicosCollection tecnico;
        private T_usuarios user;

        private decimal cost;
        private decimal advance;
        private decimal height;
        private decimal width;

        #region LunesAViernes
        private bool lvComidaChecked;
        private TimeSpan lvCheckIn;
        private TimeSpan lvCheckInEat;
        private TimeSpan lvCheckOut;
        private TimeSpan lvCheckOutEat;
        #endregion

        #region Sabado
        private bool sComidaChecked;
        private TimeSpan sCheckIn;
        private TimeSpan sCheckInEat;
        private TimeSpan sCheckOut;
        private TimeSpan sCheckOutEat;
        #endregion

        #region Domingo
        private bool dComidaChecked;
        private TimeSpan dCheckIn;
        private TimeSpan dCheckInEat;
        private TimeSpan dCheckOut;
        private TimeSpan dCheckOutEat; 
        #endregion

        public string page;
        private string activationCode;

        #endregion

        #region Properties
        private T_teccaract feature;

        public MediaFile file1;
        public MediaFile file2;
        public MediaFile file3;
        public MediaFile file4;
        public MediaFile file5;
        public MediaFile file6;
        public MediaFile file7;
        public MediaFile file8;
        public MediaFile file9;

        public string ActivationCode
        {
            get { return this.activationCode; }
            set { SetValue(ref this.activationCode, value); }
        }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }

        public bool LunVieChecked { get; set; }
        public bool SabadoChecked { get; set; }
        public bool DomingoChecked { get; set; }

        #region Small
        public string SHeight { get; set; }
        public string SWidth { get; set; }
        public string SECost { get; set; }
        public string SMCost { get; set; }
        public string SHCost { get; set; }
        public string SEAdvance { get; set; }
        public string SMAdvance { get; set; }
        public string SHAdvance { get; set; }
        public int SETime { get; set; }
        public int SMTime { get; set; }
        public int SHTime { get; set; }
        #endregion

        #region Medium
        public string MHeight { get; set; }
        public string MWidth { get; set; }
        public string MECost { get; set; }
        public string MMCost { get; set; }
        public string MHCost { get; set; }
        public string MEAdvance { get; set; }
        public string MMAdvance { get; set; }
        public string MHAdvance { get; set; }
        public int METime { get; set; }
        public int MMTime { get; set; }
        public int MHTime { get; set; }
        #endregion

        #region big
        public string BHeight { get; set; }
        public string BWidth { get; set; }
        public string BECost { get; set; }
        public string BMCost { get; set; }
        public string BHCost { get; set; }
        public string BEAdvance { get; set; }
        public string BMAdvance { get; set; }
        public string BHAdvance { get; set; }
        public int BETime { get; set; }
        public int BMTime { get; set; }
        public int BHTime { get; set; }
        #endregion

        #region LunesAViernes
        public bool LVComidaChecked
        {
            get { return this.lvComidaChecked; }
            set { SetValue(ref this.lvComidaChecked, value); }
        }
        public TimeSpan LVCheckIn
        {
            get { return this.lvCheckIn; }
            set { SetValue(ref this.lvCheckIn, value); }
        }
        public TimeSpan LVCheckInEat
        {
            get { return this.lvCheckInEat; }
            set { SetValue(ref this.lvCheckInEat, value); }
        }
        public TimeSpan LVCheckOut
        {
            get { return this.lvCheckOut; }
            set { SetValue(ref this.lvCheckOut, value); }
        }
        public TimeSpan LVCheckOutEat
        {
            get { return this.lvCheckOutEat; }
            set { SetValue(ref this.lvCheckOutEat, value); }
        }
        #endregion

        #region Sabado
        public bool SComidaChecked
        {
            get { return this.sComidaChecked; }
            set { SetValue(ref this.sComidaChecked, value); }
        }
        public TimeSpan SCheckIn
        {
            get { return this.sCheckIn; }
            set { SetValue(ref this.sCheckIn, value); }
        }
        public TimeSpan SCheckInEat
        {
            get { return this.sCheckInEat; }
            set { SetValue(ref this.sCheckInEat, value); }
        }
        public TimeSpan SCheckOut
        {
            get { return this.sCheckOut; }
            set { SetValue(ref this.sCheckOut, value); }
        }
        public TimeSpan SCheckOutEat
        {
            get { return this.sCheckOutEat; }
            set { SetValue(ref this.sCheckOutEat, value); }
        }
        #endregion

        #region Domingo
        public bool DComidaChecked
        {
            get { return this.dComidaChecked; }
            set { SetValue(ref this.dComidaChecked, value); }
        }
        public TimeSpan DCheckIn
        {
            get { return this.dCheckIn; }
            set { SetValue(ref this.dCheckIn, value); }
        }
        public TimeSpan DCheckInEat
        {
            get { return this.dCheckInEat; }
            set { SetValue(ref this.dCheckInEat, value); }
        }
        public TimeSpan DCheckOut
        {
            get { return this.dCheckOut; }
            set { SetValue(ref this.dCheckOut, value); }
        }
        public TimeSpan DCheckOutEat
        {
            get { return this.dCheckOutEat; }
            set { SetValue(ref this.dCheckOutEat, value); }
        }
        #endregion

        #endregion

        #region Constructors
        public ConfirmTecnicoPopupViewModel()
        {
            this.apiService = new ApiService();
            this.NewPassword = null;
            this.ConfirmPassword = null;
            this.ActivationCode = null;
            //this.tecnico = tecnico;
            //this.user = user;
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
        private async void ValidatePassword()
        {
            if (string.IsNullOrEmpty(this.NewPassword))
            {
                this.NewPassword = user.Pass;
            }
            //-------------- Change Password --------------//

            if (this.NewPassword == this.user.Pass)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NewPasswordError,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmPasswordError,
                    "Ok");
                return;
            }
            if (this.NewPassword != this.ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MatchPasswordError,
                    "Ok");
                return;
            }
        }
        private async void ValidateActCode()
        {
            //--------------Confirm Activation--------------//

            if (string.IsNullOrEmpty(this.ActivationCode))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ActivationCodeError,
                    "Ok");
                return;
            }
            var confirm = int.Parse(this.ActivationCode);
            if (confirm != this.user.Confirmacion)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ActivationCodeError,
                    "Ok");
                return;
            }
        }
        private async void ValidateSmall()
        {
            if (string.IsNullOrEmpty(this.SHeight) || string.IsNullOrEmpty(this.SWidth))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                return;
            }
            this.height = decimal.Parse(this.SHeight);
            this.width = decimal.Parse(this.SWidth);
            if (this.height < 0 || this.width < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.SECost)
                || string.IsNullOrEmpty(this.SMCost)
                || string.IsNullOrEmpty(this.SHCost))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                return;
            }
            this.cost = decimal.Parse(this.SECost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.SMCost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.SHCost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.SEAdvance)
                || string.IsNullOrEmpty(this.SMAdvance)
                || string.IsNullOrEmpty(this.SHAdvance))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                return;
            }
            this.advance = decimal.Parse(this.SEAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.SMAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.SHAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            if (this.SETime <= 0 || this.SMTime <= 0 || this.SMTime <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                return;
            }

            if (this.file1 == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
        }
        private async void ValidateMedium()
        {
            if (string.IsNullOrEmpty(this.MHeight) || string.IsNullOrEmpty(this.MWidth))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                return;
            }
            this.height = decimal.Parse(this.MHeight);
            this.width = decimal.Parse(this.MWidth);
            if (this.height < 0 || this.width < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.MECost)
                || string.IsNullOrEmpty(this.MMCost)
                || string.IsNullOrEmpty(this.MHCost))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                return;
            }
            this.cost = decimal.Parse(this.MECost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.MMCost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.MHCost);

            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.MEAdvance)
                || string.IsNullOrEmpty(this.MMAdvance)
                || string.IsNullOrEmpty(this.MHAdvance))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                return;
            }

            this.advance = decimal.Parse(this.MEAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.MMAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.MHAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (this.METime <= 0 || this.MMTime <= 0 || this.MMTime <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                return;
            }

            if (this.file1 == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
        }
        private async void ValidateBig()
        {
            if (string.IsNullOrEmpty(this.BHeight) || string.IsNullOrEmpty(this.BWidth))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                return;
            }
            this.height = decimal.Parse(this.BHeight);
            this.width = decimal.Parse(this.BWidth);
            if (this.height < 0 || this.width < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.BECost)
                || string.IsNullOrEmpty(this.BMCost)
                || string.IsNullOrEmpty(this.BHCost))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                return;
            }
            this.cost = decimal.Parse(this.BECost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.BMCost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.BHCost);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.BEAdvance)
                || string.IsNullOrEmpty(this.BMAdvance)
                || string.IsNullOrEmpty(this.BHAdvance))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                return;
            }
            this.advance = decimal.Parse(this.BEAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.BMAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.cost = decimal.Parse(this.BHAdvance);
            if (this.cost < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            if (this.BETime <= 0 || this.BMTime <= 0 || this.BMTime <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                return;
            }

            if (this.file1 == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
        }
        private async void SaveUserData()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {

                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "Ok");
                return;
            }

            var editUser = new T_usuarios
            {
                Id_usuario = this.user.Id_usuario,
                Bloqueo = this.user.Bloqueo,
                Confirmacion = this.user.Confirmacion,
                Confirmado = true,
                Pass = this.NewPassword,
                Tipo = this.user.Tipo,
                Ucorreo = this.user.Ucorreo,
                Usuario = this.user.Usuario,
            };
            var id = this.user.Id_usuario;

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_usuariosController"].ToString();

            this.apiService = new ApiService();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                editUser,
                id);

            if (!response.IsSuccess)
            {

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            MainViewModel.GetInstance().TecnicoFeatures = new TecnicoFeaturesViewModel(user, tecnico);
            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoFeaturesPage());
        }

        private async void SaveAndNext()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            #region Small

            this.height = decimal.Parse(this.SHeight);
            this.width = decimal.Parse(this.SWidth);

            #region Save SmallEasy

            this.cost = decimal.Parse(this.SECost);
            this.advance = decimal.Parse(this.SEAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "SmallEasy",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file1.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.SETime,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save SmallMedium

            this.apiService = new ApiService();
            this.cost = decimal.Parse(this.SMCost);
            this.advance = decimal.Parse(this.SMAdvance);
            if (this.cost < 0 || this.advance < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "SmallMedium",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file2.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.SMTime,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save SmallHard
            this.cost = decimal.Parse(this.SHCost);
            this.advance = decimal.Parse(this.SHAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "SmallHard",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file3.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.SHTime,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #endregion

            #region MediumSize

            this.height = decimal.Parse(this.MHeight);
            this.width = decimal.Parse(this.MWidth);

            #region Save MediumEasy

            this.cost = decimal.Parse(this.MECost);
            this.advance = decimal.Parse(this.MEAdvance);
            if (this.cost < 0 || this.advance < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "MediumEasy",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file4.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.METime,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save MediumMedium

            byte[] ByteImage5 = null;
            if (this.file5 == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage5 = FileHelper.ReadFully(this.file5.GetStream());

            this.cost = decimal.Parse(this.MMCost);
            this.advance = decimal.Parse(this.MMAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "MediumMedium",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file5.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = MMTime,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save MediumHard

            this.cost = decimal.Parse(this.MHCost);
            this.advance = decimal.Parse(this.MHAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "MediumHard",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file6.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.MHTime,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #endregion

            #region BigSize

            this.height = decimal.Parse(this.BHeight);
            this.width = decimal.Parse(this.BWidth);

            #region Save BigEasy

            this.cost = decimal.Parse(this.BECost);
            this.advance = decimal.Parse(this.BEAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "BigEasy",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file7.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.BETime,
            };
            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save BigMedium

            this.cost = decimal.Parse(this.BMCost);
            this.advance = decimal.Parse(this.BMAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "BigMedium",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file8.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.BMTime,
            };
            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save BigHard

            this.cost = decimal.Parse(this.BHCost);
            this.advance = decimal.Parse(this.BHAdvance);

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "BigHard",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = FileHelper.ReadFully(this.file9.GetStream()),
                Alto = this.height,
                Ancho = this.width,
                Tiempo = this.BHTime
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion 
            #endregion

            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().TecnicoHome = new TecnicoHomeViewModel(user, tecnico);
            Application.Current.MainPage = new SNavigationPage(new TecnicoHomePage())
            {
                BarBackgroundColor = Color.FromRgb(20, 20, 20),
                BarTextColor = Color.FromRgb(200, 200, 200),
            };
        }

        private async void GoToNextPopupPage()
        {
            switch (this.page)
            {
                case "Password":

                    //this.ValidatePassword();

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "ActCode";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmActCodePopupPage());
                    break;

                case "ActCode":
                    //this.ValidateActCode();
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmHorarioPopupPage());
                    break;

                case "Horario":
                    if(this.LunVieChecked==true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "LunVie";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmLunViePopupPage());
                        break;
                    }

                    if (this.SabadoChecked == true)
                    {

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSabadoPopupPage());
                        break;
                    }

                    if (this.DomingoChecked == true)
                    {

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmDomingoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Notice,
                        Languages.HorarioError,
                        "Ok");
                    break;
                case "LunVie":

                    if (this.SabadoChecked == true)
                    {

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSabadoPopupPage());
                        break;
                    }

                    if (this.DomingoChecked == true)
                    {

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmDomingoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;

                case "Sabado":

                    if (this.DomingoChecked == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmDomingoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;

                case "Domingo":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;

                case "Small":
                    //this.ValidateSmall();
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Medium";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmMediumPopupPage());
                    break;

                case "Medium":
                    //this.ValidateMedium();
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Big";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmBigPopupPage());
                    break;

                case "Big":
                    //this.ValidateBig();
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;
            }
        }
        private async void Cancel()
        {
            switch (this.page)
            {
                case "Password":
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = null;
                    this.tecnico = null;
                    this.user = null;
                    break;

                case "ActCode":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Password";
                    this.ActivationCode = null;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmPasswordPopupPage());
                    break;

                case "Horario":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "ActCode";
                    this.LunVieChecked = false;
                    this.SabadoChecked = false;
                    this.DomingoChecked = false;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmActCodePopupPage());
                    break;

                case "LunVie":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmHorarioPopupPage());
                    break;

                case "Sabado":

                    if (this.LunVieChecked == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "LunVie";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmLunViePopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmHorarioPopupPage());
                    break;

                case "Domingo":

                    if (this.SabadoChecked == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSabadoPopupPage());
                        break;
                    }

                    if (this.LunVieChecked == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "LunVie";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmLunViePopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmHorarioPopupPage());
                    break;

                case "Small":

                    if (this.DomingoChecked == true)
                    {

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmDomingoPopupPage());
                        break;
                    }

                    if (this.SabadoChecked == true)
                    {

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSabadoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "LunVie";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmLunViePopupPage());
                    break;

                case "Medium":
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;

                case "Big":
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmMediumPopupPage());
                    break;
            }
        }
        #endregion

    }
}
