namespace mLetsTatoo.Popups.ViewModel
{
    using System;
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

    public class ConfirmTecnicoPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool next;
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
        public ConfirmTecnicoPopupViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.apiService = new ApiService();
            this.NewPassword = null;
            this.ConfirmPassword = null;
            this.ActivationCode = null;
            this.tecnico = tecnico;
            this.user = user;
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
        private void ValidatePassword()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.NewPassword))
            {
                this.NewPassword = user.Pass;
            }
            //-------------- Change Password --------------//

            if (this.NewPassword == this.user.Pass)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NewPasswordError,
                    "Ok");
                this.next = false;
                return;
            }
            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmPasswordError,
                    "Ok");
                this.next = false;
                return;
            }
            if (this.NewPassword != this.ConfirmPassword)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MatchPasswordError,
                    "Ok");
                this.next = false;
                return;
            }
        }
        private void ValidateActCode()
        {
            this.next = true;
            //--------------Confirm Activation--------------//

            if (string.IsNullOrEmpty(this.ActivationCode))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ActivationCodeError,
                    "Ok");

                this.next = false;
                return;
            }
            var confirm = int.Parse(this.ActivationCode);
            if (confirm != this.user.Confirmacion)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ActivationCodeError,
                    "Ok");

                this.next = false;
                return;
            }
        }
        private void ValidateHorarios()
        {
            var horarioLocal = MainViewModel.GetInstance().Login.ListHorariosLocales.Where(h => h.Id_Local == this.tecnico.Id_Local).FirstOrDefault();
            this.next = true;
            if (this.page == "LunVie")
            {
                var checkIn = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hluvide.Hours,
                    horarioLocal.Hluvide.Minutes,0
                ).ToString("h:mm tt");

                var checkOut = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hluvia.Hours,
                    horarioLocal.Hluvia.Minutes,0
                ).ToString("h:mm tt");

                if (this.LVCheckOut.Ticks <= horarioLocal.Hluvide.Ticks || this.LVCheckOut.Ticks > horarioLocal.Hluvia.Ticks
                    || this.lvCheckIn.Ticks < horarioLocal.Hluvide.Ticks || this.LVCheckIn.Ticks >= horarioLocal.Hluvia.Ticks
                    || this.LVCheckOut.Ticks == this.LVCheckIn.Ticks)
                {
                    var message = $"{Languages.LVRangoHorarioError1} {checkIn} {Languages.To} {checkOut}. {Languages.RangoHorarioError}.";
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        message,
                        "Ok");
                    this.next = false;
                    return;
                }
                this.ValidateHorariosComida();

            }

            if (this.page == "Sabado")
            {
                var checkIn = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hsabde.Hours,
                    horarioLocal.Hsabde.Minutes,0
                ).ToString("h:mm tt");

                var checkOut = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hsaba.Hours,
                    horarioLocal.Hsaba.Minutes,0
                ).ToString("h:mm tt");

                if (this.SCheckIn.Ticks < horarioLocal.Hluvide.Ticks || this.SCheckIn.Ticks >= horarioLocal.Hluvia.Ticks
                    || this.SCheckOut.Ticks <= horarioLocal.Hluvide.Ticks || this.SCheckOut.Ticks > horarioLocal.Hluvia.Ticks
                    || this.SCheckOut.Ticks == this.SCheckIn.Ticks)
                {
                    var message = $"{Languages.SRangoHorarioError1} {checkIn} {Languages.To} {checkOut}. {Languages.RangoHorarioError}.";
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        message,
                        "Ok");
                    this.next = false;
                    return;
                }
                this.ValidateHorariosComida();
            }

            if (this.page == "Domingo")
            {
                var checkIn = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hdomde.Hours,
                    horarioLocal.Hdomde.Minutes,0
                ).ToString("h:mm tt");

                var checkOut = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hdoma.Hours,
                    horarioLocal.Hdoma.Minutes,0
                ).ToString("h:mm tt");

                if (this.DCheckIn.Ticks < horarioLocal.Hluvide.Ticks || this.DCheckIn.Ticks >= horarioLocal.Hluvia.Ticks
                    || this.DCheckOut.Ticks <= horarioLocal.Hluvide.Ticks || this.DCheckOut.Ticks > horarioLocal.Hluvia.Ticks
                    || this.DCheckOut.Ticks == this.DCheckIn.Ticks)
                {
                    var message = $"{Languages.DRangoHorarioError1} {checkIn} {Languages.To} {checkOut}. {Languages.RangoHorarioError}.";
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        message,
                        "Ok");
                    return;
                    this.next = false;
                }
                this.ValidateHorariosComida();
            }

        }
        private void ValidateHorariosComida()
        {
            var horarioLocal = MainViewModel.GetInstance().Login.ListHorariosLocales.Where(h => h.Id_Local == this.tecnico.Id_Local).FirstOrDefault();
            if (this.page == "LunVie")
            {
                if(this.LVComidaChecked == true)
                {
                    var checkIn = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        horarioLocal.Hluvide.Hours,
                        horarioLocal.Hluvide.Minutes, 0
                    ).ToString("h:mm tt");

                    var checkOut = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        horarioLocal.Hluvia.Hours,
                        horarioLocal.Hluvia.Minutes, 0
                    ).ToString("h:mm tt");

                    if (this.LVCheckOutEat.Ticks <= horarioLocal.Hluvide.Ticks || this.LVCheckOutEat.Ticks >= horarioLocal.Hluvia.Ticks
                        || this.LVCheckInEat.Ticks <= horarioLocal.Hluvide.Ticks || this.LVCheckInEat.Ticks >= horarioLocal.Hluvia.Ticks
                        || this.LVCheckOutEat == this.LVCheckInEat)
                    {
                        var message = $"{Languages.LVRangoHorarioError1} {checkIn} {Languages.To} {checkOut}. {Languages.RangoHorarioComidaError}.";
                        Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            message,
                            "Ok");
                        this.next = false;
                        return;
                    }
                }
            }

            if (this.page == "Sabado")
            {

                if (this.SComidaChecked == true)
                {
                    var checkIn = new DateTime
                    (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hsabde.Hours,
                    horarioLocal.Hsabde.Minutes, 0
                    ).ToString("h:mm tt");

                    var checkOut = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        horarioLocal.Hsaba.Hours,
                        horarioLocal.Hsaba.Minutes, 0
                    ).ToString("h:mm tt");

                    if (this.SCheckIn.Ticks <= horarioLocal.Hluvide.Ticks || this.SCheckIn.Ticks >= horarioLocal.Hluvia.Ticks
                        || this.SCheckOut.Ticks <= horarioLocal.Hluvide.Ticks || this.SCheckOut.Ticks >= horarioLocal.Hluvia.Ticks
                        || this.SCheckOutEat == this.SCheckInEat)
                    {
                        var message = $"{Languages.SRangoHorarioError1} {checkIn} {Languages.To} {checkOut}. {Languages.RangoHorarioComidaError}.";
                        Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            message,
                            "Ok");
                        this.next = false;
                        return;
                    }
                }

            }

            if (this.page == "Domingo")
            {
                if (this.DComidaChecked == true)
                {
                    var checkIn = new DateTime
                    (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    horarioLocal.Hdomde.Hours,
                    horarioLocal.Hdomde.Minutes, 0
                    ).ToString("h:mm tt");

                    var checkOut = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        horarioLocal.Hdoma.Hours,
                        horarioLocal.Hdoma.Minutes, 0
                    ).ToString("h:mm tt");

                    if (this.DCheckIn.Ticks < horarioLocal.Hluvide.Ticks || this.DCheckIn.Ticks >= horarioLocal.Hluvia.Ticks
                        || this.DCheckOut.Ticks <= horarioLocal.Hluvide.Ticks || this.DCheckOut.Ticks > horarioLocal.Hluvia.Ticks
                        || this.DCheckOutEat == this.DCheckInEat)
                    {
                        var message = $"{Languages.DRangoHorarioError1} {checkIn} {Languages.To} {checkOut}. {Languages.RangoHorarioComidaError}.";
                        Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            message,
                            "Ok");
                        return;
                        this.next = false;
                    }
                }

            }

        }
        private void ValidateSmall()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.SHeight) || string.IsNullOrEmpty(this.SWidth))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                this.next = false;
                return;
            }
            this.height = decimal.Parse(this.SHeight);
            this.width = decimal.Parse(this.SWidth);
            if (this.height < 0 || this.width < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.SECost)
                || string.IsNullOrEmpty(this.SMCost)
                || string.IsNullOrEmpty(this.SHCost))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.SECost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.SMCost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.SHCost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.SEAdvance)
                || string.IsNullOrEmpty(this.SMAdvance)
                || string.IsNullOrEmpty(this.SHAdvance))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                this.next = false;
                return;
            }
            this.advance = decimal.Parse(this.SEAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.SMAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.SHAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.SETime <= 0 || this.SMTime <= 0 || this.SMTime <= 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                this.next = false;
                return;
            }

            if (this.file1 == null)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                this.next = false;
                return;
            }
        }
        private void ValidateMedium()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.MHeight) || string.IsNullOrEmpty(this.MWidth))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                this.next = false;
                return;
            }
            this.height = decimal.Parse(this.MHeight);
            this.width = decimal.Parse(this.MWidth);
            if (this.height < 0 || this.width < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.MECost)
                || string.IsNullOrEmpty(this.MMCost)
                || string.IsNullOrEmpty(this.MHCost))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.MECost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.MMCost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.MHCost);

            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.MEAdvance)
                || string.IsNullOrEmpty(this.MMAdvance)
                || string.IsNullOrEmpty(this.MHAdvance))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                this.next = false;
                return;
            }

            this.advance = decimal.Parse(this.MEAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.MMAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.MHAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (this.METime <= 0 || this.MMTime <= 0 || this.MMTime <= 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                this.next = false;
                return;
            }

            if (this.file1 == null)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                this.next = false;
                return;
            }
        }
        private void ValidateBig()
        {
            this.next = true;
            if (string.IsNullOrEmpty(this.BHeight) || string.IsNullOrEmpty(this.BWidth))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                this.next = false;
                return;
            }
            this.height = decimal.Parse(this.BHeight);
            this.width = decimal.Parse(this.BWidth);
            if (this.height < 0 || this.width < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.BECost)
                || string.IsNullOrEmpty(this.BMCost)
                || string.IsNullOrEmpty(this.BHCost))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.BECost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.BMCost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.BHCost);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }

            if (string.IsNullOrEmpty(this.BEAdvance)
                || string.IsNullOrEmpty(this.BMAdvance)
                || string.IsNullOrEmpty(this.BHAdvance))
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                this.next = false;
                return;
            }
            this.advance = decimal.Parse(this.BEAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.BMAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            this.cost = decimal.Parse(this.BHAdvance);
            if (this.cost < 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                this.next = false;
                return;
            }
            if (this.BETime <= 0 || this.BMTime <= 0 || this.BMTime <= 0)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EstimatedTimeError,
                    "OK");
                this.next = false;
                return;
            }

            if (this.file1 == null)
            {
                Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                this.next = false;
                return;
            }
        }
        private void ValidateHorarioAct()
        {
            var horarioLocal = MainViewModel.GetInstance().Login.ListHorariosLocales.Where(h => h.Id_Local == this.tecnico.Id_Local).FirstOrDefault();
            this.next = true;
            if (this.LunVieChecked == true)
            {
                if (horarioLocal.Hluviact == false)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.LunVieActError,
                        "Ok");
                    this.next = false;
                    return;
                }
            }
            if (this.SabadoChecked == true)
            {

                if (horarioLocal.Hsabact == false)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.SabActError,
                        "Ok");
                    this.next = false;
                    return;
                }
            }
            if (this.DomingoChecked == true)
            {
                if (horarioLocal.Hdomact == false)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.DomActError,
                        "Ok");
                    this.next = false;
                    return;
                }
            }
        }
        private async void SaveData()
        {
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

            //------------------------- Save User Changes ----------------------//

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
                this.apiService.EndActivityPopup();

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            this.user = (T_usuarios)response.Result;

            var oldUser = MainViewModel.GetInstance().Login.ListUsuarios.Where(n => n.Id_usuario == this.user.Id_usuario).FirstOrDefault();
            if (oldUser != null)
            {
                MainViewModel.GetInstance().Login.ListUsuarios.Remove(oldUser);
            }

            MainViewModel.GetInstance().Login.ListUsuarios.Add(this.user);

            //------------------------- Save Horarios ----------------------//

            var newHorario = new T_tecnicohorarios
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Hdoma = this.DCheckOut,
                Hdomact = this.DomingoChecked,
                Hdomde = this.DCheckIn,
                Hdcomidaa = this.DCheckInEat,
                Hdcomidaact = this.DComidaChecked,
                Hdcomidade = this.DCheckOutEat,
                Hluvia = this.LVCheckOut,
                Hluviact = this.LunVieChecked,
                Hluvide = this.LVCheckIn,
                Hlvcomidaa = this.LVCheckInEat,
                Hlvcomidaact = this.LVComidaChecked,
                Hlvcomidade = this.LVCheckOutEat,
                Hsaba = this.SCheckOut,
                Hsabact = this.SabadoChecked,
                Hsabde = this.SCheckIn,
                Hscomidaa = this.SCheckInEat,
                Hscomidaact = this.SComidaChecked,
                Hscomidade = this.SCheckOutEat,
            };

            controller = Application.Current.Resources["UrlT_tecnicohorariosController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newHorario);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newHorario = (T_tecnicohorarios)response.Result;
            MainViewModel.GetInstance().Login.ListHorariosTecnicos.Add(newHorario);

            //------------------------- Save Features ----------------------//

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

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
            #endregion

            #region Save SmallMedium

            this.cost = decimal.Parse(this.SMCost);
            this.advance = decimal.Parse(this.SMAdvance);

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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
            #endregion

            #endregion

            #region MediumSize

            this.height = decimal.Parse(this.MHeight);
            this.width = decimal.Parse(this.MWidth);

            #region Save MediumEasy

            this.cost = decimal.Parse(this.MECost);
            this.advance = decimal.Parse(this.MEAdvance);

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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
            #endregion

            #region Save MediumMedium

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

                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
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
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newfeature = (T_teccaract)response.Result;
            MainViewModel.GetInstance().Login.FeaturesList.Add(newfeature);
            #endregion
            #endregion

            MainViewModel.GetInstance().RergisterDevice();
            MainViewModel.GetInstance().TecnicoHome = new TecnicoHomeViewModel(this.user, this.tecnico);
            Application.Current.MainPage = new SNavigationPage(new TecnicoHomePage())
            {
                BarBackgroundColor = Color.FromRgb(20, 20, 20),
                BarTextColor = Color.FromRgb(200, 200, 200),
            };

            this.apiService.EndActivityPopup();
        }

        private async void GoToNextPopupPage()
        {
            switch (this.page)
            {
                case "Password":

                    this.ValidatePassword();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "ActCode";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmActCodePopupPage());
                    break;

                case "ActCode":
                    //this.ValidateActCode();
                    //if (this.next == false)
                    //{
                    //    break;
                    //}
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmHorarioPopupPage());
                    break;

                case "Horario":

                    this.ValidateHorarioAct();

                    if (this.next == false)
                    {
                        break;
                    }

                    if (this.LunVieChecked == true)
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
                    this.ValidateHorarios();

                    if(this.next == false)
                    {
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

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;

                case "Sabado":
                    this.ValidateHorarios();

                    if (this.next == false)
                    {
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

                case "Domingo":
                    this.ValidateHorarios();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Small";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmSmallPopupPage());
                    break;

                case "Small":
                    this.ValidateSmall();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Medium";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmMediumPopupPage());
                    break;

                case "Medium":
                    this.ValidateMedium();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Big";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmBigPopupPage());
                    break;

                case "Big":
                    this.ValidateBig();

                    if (this.next == false)
                    {
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();

                    this.SaveData();

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
