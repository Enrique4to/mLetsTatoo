namespace mLetsTatoo.Popups.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using ViewModels;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using System;
    using Xamarin.Forms;
    using Rg.Plugins.Popup.Extensions;
    using mLetsTatoo.Popups.Views;
    using System.IO;
    using Plugin.Media.Abstractions;
    using Plugin.Media;

    public class NewAppointmentPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public DateTime selectedDate;
        public DateTime selectedTime;

        private TimeSpan workIn;
        private TimeSpan workOut;
        private TimeSpan eatIn;
        private TimeSpan eatOut;

        public bool typeAppointment; //<-- QuickDate value = 1, Personalized Date Value = 0
        private bool smallChecked;
        private bool mediumSizeChecked;
        private bool bigChecked;
        private bool easyChecked;
        private bool mediumComplexityChecked;
        private bool hardChecked;
        private bool isRefreshing;

        public decimal cost;
        public decimal advance;

        public string appointmentType;
        public string thisPage;
        public string filter;
        private string heightWidth;
        private string appCost;
        private string appAdvance;
        private string appDuration;
        private string complexity;
        private string describeArt;

        public string workInTime;
        public string workOutTime;
        public string eatInTime;
        public string eatOutTime;

        private ImageSource featureImageSource;
        private ImageSource artImageSource;

        private MediaFile file;

        public T_teccaract feature;
        public TecnicosCollection tecnico;
        public ClientesCollection cliente;
        private CitasItemViewModel newCita;

        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private ObservableCollection<CitasItemViewModel> citas;
        #endregion

        #region Properties
        public DateTime SelectedDate
        {
            get { return this.selectedDate; }
            set { SetValue(ref this.selectedDate, value); }
        }
        public DateTime SelectedTime
        {
            get { return this.selectedTime; }
            set { SetValue(ref this.selectedTime, value); }
        }

        public bool TypeAppointment
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
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

        public string AppointmentType
        {
            get { return this.appointmentType; }
            set { SetValue(ref this.appointmentType, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshTecnicoList();
            }
        }
        public string HeightWidth
        {
            get { return this.heightWidth; }
            set { SetValue(ref this.heightWidth, value); }
        }
        public string AppCost
        {
            get { return this.appCost; }
            set { SetValue(ref this.appCost, value); }
        }
        public string AppAdvance
        {
            get { return this.appAdvance; }
            set { SetValue(ref this.appAdvance, value); }
        }
        public string AppDuration
        {
            get { return this.appDuration; }
            set { SetValue(ref this.appDuration, value); }
        }
        public string DescribeArt
        {
            get { return this.describeArt; }
            set { SetValue(ref this.describeArt, value); }
        }

        public string WorkInTime
        {
            get { return this.workInTime; }
            set { SetValue(ref this.workInTime, value); }
        }
        public string WorkOutTime
        {
            get { return this.workOutTime; }
            set { SetValue(ref this.workOutTime, value); }
        }
        public string EatInTime
        {
            get { return this.eatInTime; }
            set { SetValue(ref this.eatInTime, value); }
        }
        public string EatOutTime
        {
            get { return this.eatOutTime; }
            set { SetValue(ref this.eatOutTime, value); }
        }

        public decimal Cost
        {
            get { return this.cost; }
            set { SetValue(ref this.cost, value); }
        }
        public decimal Advance
        {
            get { return this.advance; }
            set { SetValue(ref this.advance, value); }
        }

        public ImageSource FeatureImageSource
        {
            get { return this.featureImageSource; }
            set { SetValue(ref this.featureImageSource, value); }
        }
        public ImageSource ArtImageSource
        {
            get { return this.artImageSource; }
            set { SetValue(ref this.artImageSource, value); }
        }
        
        public List<TecnicosCollection> TecnicoList { get; set; }
        public List<CitasItemViewModel> CitaList { get; set; }

        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
        }
        public ObservableCollection<CitasItemViewModel> Citas
        {
            get { return this.citas; }
            set { SetValue(ref this.citas, value); }
        }
        #endregion

        #region Constructors
        public NewAppointmentPopupViewModel(ClientesCollection cliente)
        {
            this.cliente = cliente;
            this.apiService = new ApiService();            
            this.RefreshTecnicoList();
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshTecnicoList);
            }
        }
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
        public ICommand SelectedTimeCommand
        {
            get
            {
                return new RelayCommand(NewDateSelected);
            }
        }
        public ICommand AddArtImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        #endregion

        #region Methods
        public void RefreshTecnicoList()
        {
            this.TecnicoList = MainViewModel.GetInstance().Login.TecnicoList;
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            this.TecnicoList = this.TecnicoList.Where(t => userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)).ToList();
            if (string.IsNullOrEmpty(this.filter))
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido = t.Apellido,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                    F_Perfil = t.F_Perfil

                });
                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }
            else
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido = t.Apellido,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                    F_Perfil = t.F_Perfil

                }).Where(t => t.Nombre.ToLower().Contains(this.filter.ToLower())
                || t.Apodo.ToLower().Contains(this.filter.ToLower())
                || t.Apellido.ToLower().Contains(this.filter.ToLower())).ToList();

                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }
        }
        public void LoadFeatures(object sender)
        {           
            var list = MainViewModel.GetInstance().UserHome.ListFeature.Where(f => f.Id_Tecnico == this.tecnico.Id_Tecnico).ToList();

            if (SmallChecked == true && EasyChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "SmallEasy");
                this.complexity = Languages.Easy;
            }
            else if (SmallChecked == true && MediumComplexityChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "SmallMedium");
                this.complexity = Languages.MediumComplexity;
            }
            else if (SmallChecked == true && HardChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "SmallHard");
                this.complexity = Languages.Hard;
            }

            if (MediumSizeChecked == true && EasyChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "MediumEasy");
                this.complexity = Languages.Easy;
            }
            else if (MediumSizeChecked == true && MediumComplexityChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "MediumMedium");
                this.complexity = Languages.MediumComplexity;
            }
            else if (MediumSizeChecked == true && HardChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "MediumHard");
                this.complexity = Languages.Hard;
            }

            if (BigChecked == true && EasyChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "BigEasy");
                this.complexity = Languages.Easy;
            }
            else if (BigChecked == true && MediumComplexityChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "BigMedium");
                this.complexity = Languages.MediumComplexity;
            }
            else if (BigChecked == true && HardChecked == true)
            {
                this.feature = list.Single(f => f.Caract == "BigHard");
                this.complexity = Languages.Hard;
            }

            this.Cost = this.feature.Total_Aprox;
            this.Advance = this.feature.Costo_Cita;

            if (this.feature.Imagen_Ejemplo != null)
            {
                this.FeatureImageSource = ImageSource.FromStream(() => new MemoryStream(this.feature.Imagen_Ejemplo));
            }
            string tempTime = null;
            if (this.feature.Tiempo == 30)
                tempTime = "30 mins.";
            if (this.feature.Tiempo == 60)
                tempTime = "1 hr.";
            if (this.feature.Tiempo == 90)
                tempTime = "1.5 hrs.";
            if (this.feature.Tiempo == 120)
                tempTime = "2 hrs.";
            if (this.feature.Tiempo == 150)
                tempTime = "2.5 hrs.";
            if (this.feature.Tiempo == 180)
                tempTime = "3 hrs.";

            this.HeightWidth = $"{Languages.MaximunSize}: {this.feature.Alto} cm X {this.feature.Ancho} cm";
            this.AppCost = $"{Languages.Cost}: {this.feature.Total_Aprox.ToString("C2")}";
            this.AppAdvance = $"{Languages.Advance}: {this.feature.Costo_Cita.ToString("C2")}";
            this.AppDuration = $"{Languages.EstimatedTime} {tempTime}";
        }
        public void LoadCitas()
        {
            this.CitaList = MainViewModel.GetInstance().UserHome.CitaList.Select(c => new CitasItemViewModel
            {
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,
                Id_Cliente = c.Id_Cliente,
                Id_Tatuador = c.Id_Tatuador,
                F_Inicio = new DateTime(c.F_Inicio.Year, c.F_Inicio.Month, c.F_Inicio.Day, c.H_Inicio.Hours, c.H_Inicio.Minutes, c.H_Inicio.Seconds),
                H_Inicio = c.H_Inicio,
                F_Fin = new DateTime(c.F_Fin.Year, c.F_Fin.Month, c.F_Fin.Day, c.H_Fin.Hours, c.H_Fin.Minutes, c.H_Fin.Seconds),
                H_Fin = c.H_Fin,
                Asunto = c.Asunto,
                Completa = c.Completa,
                Color = Color.Red,

            }).Where(c => c.Completa == false && c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();

            this.Citas = new ObservableCollection<CitasItemViewModel>(this.CitaList.OrderByDescending(c => c.F_Inicio));
        }
        private async void NewDateSelected()
        {
            if (newCita != null)
            {
                MainViewModel.GetInstance().UserHome.CitaList.Remove(this.newCita);
                this.newCita = null;
                this.LoadCitas();
            }

            var times = (feature.Tiempo / 30) - 1;
            var TempDate = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);
            var tempTime = new TimeSpan(TempDate.Hour, TempDate.Minute, TempDate.Second);
            var tempStartDate = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, 0, 0, 0);
            var tempEndDate = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, 23, 59, 59);
            var tempCitaList = this.CitaList.Where(c => c.F_Inicio.Ticks > tempStartDate.Ticks && c.F_Inicio.Ticks < tempEndDate.Ticks).ToList();

            foreach(var cita in tempCitaList)
            {
                for (int i = 0; i <= times; i++)
                {
                    if (tempTime.Ticks >= cita.H_Inicio.Ticks && tempTime.Ticks < cita.H_Fin.Ticks)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.ScheduleError,
                            "OK");
                        if (newCita != null)
                        {
                            MainViewModel.GetInstance().UserHome.CitaList.Remove(this.newCita);
                            this.newCita = null;
                            this.LoadCitas();
                        }
                        return;
                    }
                    if (tempTime.Ticks < this.workIn.Ticks || tempTime.Ticks >= this.workOut.Ticks)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.ScheduleError,
                            "OK");
                        if (newCita != null)
                        {
                            MainViewModel.GetInstance().UserHome.CitaList.Remove(this.newCita);
                            this.newCita = null;
                            this.LoadCitas();
                        }
                        return;
                    }
                    if (tempTime.Ticks >= this.eatOut.Ticks && tempTime.Ticks < this.eatIn.Ticks)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.ScheduleError,
                            "OK");
                        if (newCita != null)
                        {
                            MainViewModel.GetInstance().UserHome.CitaList.Remove(this.newCita);
                            this.newCita = null;
                            this.LoadCitas();
                        }
                        return;
                    }

                    TempDate = TempDate.AddMinutes(30.00);
                    tempTime = new TimeSpan(TempDate.Hour, TempDate.Minute, TempDate.Second);
                }
            }
            
            var lastCita = MainViewModel.GetInstance().UserHome.CitaList.Last();
            var newEndDate = SelectedTime.AddMinutes(this.feature.Tiempo);
            
            this.newCita = new CitasItemViewModel
            {
                Id_Cita = lastCita.Id_Cita + 1,
                Id_Trabajo = lastCita.Id_Trabajo,
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                F_Inicio = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                H_Inicio = new TimeSpan(SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                F_Fin = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                H_Fin = new TimeSpan(newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                Asunto = "Tu nueva Cita",
                Completa = false,
                Color = Color.Green,
            };

            if (newCita != null)
            {
                MainViewModel.GetInstance().UserHome.CitaList.Remove(newCita);
            }

            MainViewModel.GetInstance().UserHome.CitaList.Add(newCita);

            this.LoadCitas();
        }
        public void LoadHorarios()
        {
            var horario = MainViewModel.GetInstance().UserHome.ListHorariosTecnicos.Single(h => h.Id_Tecnico == this.tecnico.Id_Tecnico);
            if(SelectedDate.DayOfWeek == DayOfWeek.Monday
                || SelectedDate.DayOfWeek == DayOfWeek.Tuesday
                || SelectedDate.DayOfWeek == DayOfWeek.Wednesday
                || SelectedDate.DayOfWeek == DayOfWeek.Thursday
                || SelectedDate.DayOfWeek == DayOfWeek.Friday)
            {
                if(horario.Hluviact == true)
                {
                    this.WorkInTime = $"{horario.Hluvide.Hours.ToString()}.{horario.Hluvide.Minutes.ToString()}";
                    this.workIn = new TimeSpan(horario.Hluvide.Hours, horario.Hluvide.Minutes, 0);
                    this.WorkOutTime = $"{horario.Hluvia.Hours.ToString()}.{horario.Hluvia.Minutes.ToString()}";
                    this.workOut = new TimeSpan(horario.Hluvia.Hours, horario.Hluvia.Minutes, 0);

                    if (horario.Hcomida == true)
                    {
                        this.EatInTime = $"{horario.Hcomidade.Hours.ToString()}";
                        this.eatIn = new TimeSpan(horario.Hcomidade.Hours, horario.Hcomidade.Minutes, 0);
                        this.EatOutTime = $"{horario.Hcomidaa.Hours.ToString()}";
                        this.eatOut = new TimeSpan(horario.Hcomidaa.Hours, horario.Hcomidaa.Minutes, 0);
                    }
                    else
                    {
                        this.EatInTime = "";
                        this.EatOutTime = "";
                    }
                }
                else
                {
                    this.EatInTime = "00";
                    this.EatOutTime = "23";
                }
            }
            else if(SelectedDate.DayOfWeek == DayOfWeek.Saturday)
            {
                if(horario.Hsabact == true)
                {
                    this.WorkInTime = $"{horario.Hsabde.Hours.ToString()}.{horario.Hsabde.Minutes.ToString()}";
                    this.workIn = new TimeSpan(horario.Hsabde.Hours, horario.Hsabde.Minutes, 0);
                    this.WorkOutTime = $"{horario.Hsaba.Hours.ToString()}.{horario.Hsaba.Minutes.ToString()}";
                    this.workOut = new TimeSpan(horario.Hsaba.Hours, horario.Hsaba.Minutes, 0);

                    if (horario.Hcomida == true)
                    {
                        this.EatInTime = $"{horario.Hcomidade.Hours.ToString()}";
                        this.eatIn = new TimeSpan(horario.Hcomidade.Hours, horario.Hcomidade.Minutes, 0);
                        this.EatOutTime = $"{horario.Hcomidaa.Hours.ToString()}";
                        this.eatOut = new TimeSpan(horario.Hcomidaa.Hours, horario.Hcomidaa.Minutes, 0);
                    }
                    else
                    {
                        this.EatInTime = "";
                        EatOutTime = "";
                    }
                }
                else
                {
                    this.EatInTime = "00";
                    this.EatOutTime = "23";
                }
            }
            else if (SelectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                if (horario.Hdomact == true)
                {
                    this.WorkInTime = $"{horario.Hdomde.Hours.ToString()}.{horario.Hdomde.Minutes.ToString()}";
                    this.workIn = new TimeSpan(horario.Hdomde.Hours, horario.Hdomde.Minutes, 0);
                    this.WorkOutTime = $"{horario.Hdoma.Hours.ToString()}.{horario.Hdoma.Minutes.ToString()}";
                    this.workOut = new TimeSpan(horario.Hdoma.Hours, horario.Hdoma.Minutes, 0);

                    if (horario.Hcomida == true)
                    {
                        this.EatInTime = $"{horario.Hcomidade.Hours.ToString()}";
                        this.eatIn = new TimeSpan(horario.Hcomidade.Hours, horario.Hcomidade.Minutes, 0);
                        this.EatOutTime = $"{horario.Hcomidaa.Hours.ToString()}";
                        this.eatOut = new TimeSpan(horario.Hcomidaa.Hours, horario.Hcomidaa.Minutes, 0);
                    }
                    else
                    {
                        this.EatInTime = "";
                        this.EatOutTime = "";
                    }
                }
                else
                {
                    this.EatInTime = "00";
                    this.EatOutTime = "23";
                }
            }
        }
        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.WhereTakePicture,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    });
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync(
                    new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Small,
                    });
            }

            if (this.file != null)
            {
                this.ArtImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }


        private async void GoToNextPopupPage()
        {
            switch (this.thisPage)
            {
                case "Search":

                    if (this.tecnico == null)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.SelectedArtistError,
                        "Ok");
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Type";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new TypeAppointmentPopupPage());
                    break;

                case "Type":

                    if (this.typeAppointment == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.thisPage = "Feature";
                        this.smallChecked = true;
                        this.easyChecked = true;
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentFeaturesPopupPage());
                        break;
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.thisPage = "Description";
                        break;
                    }

                case "Feature":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Calendar";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
                    break;

                case "Calendar":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.LoadCitas();
                    this.LoadHorarios();
                    this.SelectedDate = this.selectedDate.AddHours(8);
                    this.thisPage = "Time";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentTimePopupPage());
                    break;

                case "Time":
                    if (this.newCita == null)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.TimeError,
                        "Ok");
                        break;
                    }
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Description";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDescriptionPopupPage());
                    break;

                case "Description":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Description";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDescriptionPopupPage());
                    break;
            }
        }
        private async void Cancel()
        {
            switch (this.thisPage)
            {
                case "Search":

                    this.tecnico = null;
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = null;
                    break;

                case "Type":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Search";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new SearchTecnicoPopupPage());
                    break;

                case "Feature":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Type";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new TypeAppointmentPopupPage());
                    break;

                case "Calendar":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Feature";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentFeaturesPopupPage());
                    break;

                case "Time":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    if (this.newCita != null)
                    {
                        MainViewModel.GetInstance().UserHome.CitaList.Remove(this.newCita);
                        this.LoadCitas();
                    }
                    this.CitaList = null;
                    this.thisPage = "Calendar";
                    this.SelectedDate = DateTime.Now;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
                    break;
            }
        }
        #endregion
    }
}
