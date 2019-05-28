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

    public class EditSchedulerPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        private bool next;
        private T_tecnicohorarios horario;
        private T_localhorarios localHorario;
        public string page;

        #endregion

        #region Properties
        public T_tecnicohorarios Horario
        {
            get { return this.horario; }
            set { SetValue(ref this.horario, value); }
        }        
        #endregion

        #region Constructors
        public EditSchedulerPopupViewModel(T_tecnicohorarios horario, T_localhorarios localHorario)
        {
            this.apiService = new ApiService();
            this.horario = horario;
            this.localHorario = localHorario;
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
        private void ValidateHorarios()
        {
            this.next = true;
            if (this.page == "LunVie")
            {
                var checkIn = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    this.localHorario.Hluvide.Hours,
                    this.localHorario.Hluvide.Minutes, 0
                ).ToString("h:mm tt");

                var checkOut = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    this.localHorario.Hluvia.Hours,
                    this.localHorario.Hluvia.Minutes, 0
                ).ToString("h:mm tt");

                if (this.Horario.Hluvide.Ticks < this.localHorario.Hluvide.Ticks || this.Horario.Hluvide.Ticks >= this.localHorario.Hluvia.Ticks
                    || this.Horario.Hluvia.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hluvia.Ticks > this.localHorario.Hluvia.Ticks
                    || this.Horario.Hluvide.Ticks == this.Horario.Hluvia.Ticks)
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
                    this.localHorario.Hsabde.Hours,
                    this.localHorario.Hsabde.Minutes, 0
                ).ToString("h:mm tt");

                var checkOut = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    this.localHorario.Hsaba.Hours,
                    this.localHorario.Hsaba.Minutes, 0
                ).ToString("h:mm tt");

                if (this.Horario.Hsabde.Ticks < this.localHorario.Hluvide.Ticks || this.Horario.Hsabde.Ticks >= this.localHorario.Hluvia.Ticks
                    ||this.Horario.Hsaba.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hsaba.Ticks > this.localHorario.Hluvia.Ticks
                    || this.Horario.Hsabde.Ticks == this.Horario.Hsaba.Ticks)
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
                    this.localHorario.Hdomde.Hours,
                    this.localHorario.Hdomde.Minutes, 0
                ).ToString("h:mm tt");

                var checkOut = new DateTime
                (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    this.localHorario.Hdoma.Hours,
                    this.localHorario.Hdoma.Minutes, 0
                ).ToString("h:mm tt");

                if (this.Horario.Hdomde.Ticks < this.localHorario.Hluvide.Ticks || this.Horario.Hdomde.Ticks >= this.localHorario.Hluvia.Ticks
                    || this.Horario.Hdoma.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hdoma.Ticks > this.localHorario.Hluvia.Ticks
                    || this.Horario.Hdomde.Ticks == this.Horario.Hdoma.Ticks)
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
            if (this.page == "LunVie")
            {
                if (this.Horario.Hlvcomidaact == true)
                {
                    var checkIn = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        this.localHorario.Hluvide.Hours,
                        this.localHorario.Hluvide.Minutes, 0
                    ).ToString("h:mm tt");

                    var checkOut = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        this.localHorario.Hluvia.Hours,
                        this.localHorario.Hluvia.Minutes, 0
                    ).ToString("h:mm tt");

                    if (this.Horario.Hlvcomidade.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hlvcomidade.Ticks >= this.localHorario.Hluvia.Ticks
                        || this.Horario.Hlvcomidaa.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hlvcomidaa.Ticks >= this.localHorario.Hluvia.Ticks
                        || this.Horario.Hlvcomidade == this.Horario.Hlvcomidaa)
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

                if (this.Horario.Hscomidaact == true)
                {
                    var checkIn = new DateTime
                    (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    this.localHorario.Hsabde.Hours,
                    this.localHorario.Hsabde.Minutes, 0
                    ).ToString("h:mm tt");

                    var checkOut = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        this.localHorario.Hsaba.Hours,
                        this.localHorario.Hsaba.Minutes, 0
                    ).ToString("h:mm tt");

                    if (this.Horario.Hscomidaa.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hscomidaa.Ticks >= this.localHorario.Hluvia.Ticks
                        || this.Horario.Hscomidade.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hscomidade.Ticks >= this.localHorario.Hluvia.Ticks
                        || this.Horario.Hscomidade == this.Horario.Hscomidaa)
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
                if (this.Horario.Hdcomidaact == true)
                {
                    var checkIn = new DateTime
                    (
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    this.localHorario.Hdomde.Hours,
                    this.localHorario.Hdomde.Minutes, 0
                    ).ToString("h:mm tt");

                    var checkOut = new DateTime
                    (
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        this.localHorario.Hdoma.Hours,
                        this.localHorario.Hdoma.Minutes, 0
                    ).ToString("h:mm tt");

                    if (this.Horario.Hdcomidaa.Ticks < this.localHorario.Hluvide.Ticks || this.Horario.Hdcomidaa.Ticks >= this.localHorario.Hluvia.Ticks
                        || this.Horario.Hdcomidade.Ticks <= this.localHorario.Hluvide.Ticks || this.Horario.Hdcomidade.Ticks > this.localHorario.Hluvia.Ticks
                        || this.Horario.Hdcomidade == this.Horario.Hdcomidaa)
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
        private void ValidateHorarioAct()
        {
            this.next = true;
            if (this.Horario.Hluviact == true)
            {
                if (this.localHorario.Hluviact == false)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.LunVieActError,
                        "Ok");
                    this.next = false;
                    return;
                }
            }
            if (this.Horario.Hsabact == true)
            {

                if (this.localHorario.Hsabact == false)
                {
                    Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.SabActError,
                        "Ok");
                    this.next = false;
                    return;
                }
            }
            if (this.Horario.Hdomact == true)
            {
                if (this.localHorario.Hdomact == false)
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
            var controller = App.Current.Resources["UrlT_tecnicohorariosController"].ToString();

            this.apiService = new ApiService();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                this.Horario,
                this.Horario.Id_Horario);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newHorario = (T_tecnicohorarios)response.Result;

            var oldHorario = MainViewModel.GetInstance().Login.ListHorariosTecnicos.Where(n => n.Id_Horario == this.Horario.Id_Horario).FirstOrDefault();
            if (oldHorario != null)
            {
                MainViewModel.GetInstance().Login.ListHorariosTecnicos.Remove(oldHorario);
            }

            MainViewModel.GetInstance().Login.ListHorariosTecnicos.Add(newHorario);
            MainViewModel.GetInstance().TecnicoHome.horario = newHorario;
        }

        private async void GoToNextPopupPage()
        {
            switch (this.page)
            {
                case "Horario":

                    this.ValidateHorarioAct();

                    if (this.next == false)
                    {
                        break;
                    }

                    if (this.Horario.Hluviact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "LunVie";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditLunViePopupPage());
                        break;
                    }

                    if (this.Horario.Hsabact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditSabadoPopupPage());
                        break;
                    }

                    if (this.Horario.Hdomact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditDomingoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Notice,
                        Languages.HorarioError,
                        "Ok");
                    break;

                case "LunVie":
                    this.ValidateHorarios();

                    if (this.next == false)
                    {
                        break;
                    }

                    if (this.Horario.Hsabact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditSabadoPopupPage());
                        break;
                    }

                    if (this.Horario.Hdomact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditDomingoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.SaveData();
                    break;

                case "Sabado":
                    this.ValidateHorarios();

                    if (this.next == false)
                    {
                        break;
                    }

                    if (this.Horario.Hdomact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Domingo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditDomingoPopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.SaveData();
                    break;

                case "Domingo":
                    this.ValidateHorarios();

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
                case "Horario":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = null;
                    break;

                case "LunVie":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditHorarioPopupPage());
                    break;

                case "Sabado":

                    if (this.Horario.Hluviact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "LunVie";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditLunViePopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditHorarioPopupPage());
                    break;

                case "Domingo":

                    if (this.Horario.Hsabact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "Sabado";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditSabadoPopupPage());
                        break;
                    }

                    if (this.Horario.Hluviact == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.page = "LunVie";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new EditLunViePopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.page = "Horario";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new EditHorarioPopupPage());
                    break;
            }
        }
        #endregion

    }
}
