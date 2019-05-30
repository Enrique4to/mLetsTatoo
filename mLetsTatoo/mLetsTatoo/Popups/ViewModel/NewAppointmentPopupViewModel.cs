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
    using PayPal.Forms;
    using PayPal.Forms.Abstractions;
    using System.Diagnostics;

    public class NewAppointmentPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public DateTime selectedDate;
        public DateTime selectedTime;
        public string hourStart;
        public string dateStart;

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
        private bool isVisible;
        private bool isVisible2;
        private bool cashMetodoChecked;
        private bool payPalMetodoChecked;
        private bool SaldoFavorPayMethod;

        public bool fromTecnitoPage;
        public bool PresupuestoPage;
        public bool changeDate;
        public bool addNewDate;

        public decimal cost;
        public decimal advance;
        public decimal newSaldoFavor;
        public decimal tempAdvance;
        public decimal comision;

        public string appointmentType;
        public string thisPage;
        public string filter;
        private string heightWidth;
        private string appCost;
        private string appAdvance;
        private string appDuration;
        private string payPalComission;
        private string complexity;
        private string describeArt;
        public string height;
        public string width;

        private string address;
        private string studio;
        private string reference;
        private string artist;

        private string subject;

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
        private T_locales local;
        private T_empresas empresa;
        private T_estado estado;
        private T_ciudad ciudad;
        private T_postal postal;
        private T_trabajos trabajo;

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
        public string HourStart
        {
            get { return this.hourStart; }
            set { SetValue(ref this.hourStart, value); }
        }
        public string DateStart
        {
            get { return this.dateStart; }
            set { SetValue(ref this.dateStart, value); }
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
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsVisible2
        {
            get { return this.isVisible2; }
            set { SetValue(ref this.isVisible2, value); }
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
        public bool CashMetodoChecked
        {
            get { return this.cashMetodoChecked; }
            set { SetValue(ref this.cashMetodoChecked, value); }
        }
        public bool PayPalMetodoChecked
        {
            get { return this.payPalMetodoChecked; }
            set { SetValue(ref this.payPalMetodoChecked, value); }
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
        public string PayPalComission
        {
            get { return this.payPalComission; }
            set { SetValue(ref this.payPalComission, value); }
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
        public string Height
        {
            get { return this.height; }
            set { SetValue(ref this.height, value); }
        }
        public string Width
        {
            get { return this.width; }
            set { SetValue(ref this.width, value); }
        }

        public string Address
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }
        public string Studio
        {
            get { return this.studio; }
            set { SetValue(ref this.studio, value); }
        }
        public string Reference
        {
            get { return this.reference; }
            set { SetValue(ref this.reference, value); }
        }
        public string Artist
        {
            get { return this.artist; }
            set { SetValue(ref this.artist, value); }
        }

        public string Subject
        {
            get { return this.subject; }
            set { SetValue(ref this.subject, value); }
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
        
        public TecnicosCollection Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }

        public List<TecnicosCollection> TecnicoList { get; set; }
        public List<CitasItemViewModel> CitaList { get; set; }
        public List<NotasTempItemViewModel> TrabajoNotaList { get; set; }

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
            this.PresupuestoPage = false;
            this.changeDate = false;
            this.IsRefreshing = false;
            this.IsVisible = false;
            this.SaldoFavorPayMethod = false;
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
        public ICommand GoToUrlCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

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
                    Id_Usuario = t.Id_Usuario,
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
                    Id_Usuario = t.Id_Usuario,
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
            var list = MainViewModel.GetInstance().UserHome.FeaturesList.Where(f => f.Id_Tecnico == this.tecnico.Id_Tecnico).ToList();

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
            switch (this.feature.Tiempo)
            {
                case 30:
                    tempTime = "30 mins.";
                    break;
                case 60:
                    tempTime = "1 hr.";
                    break;
                case 90:
                    tempTime = "1.5 hrs.";
                    break;
                case 120:
                    tempTime = "2 hrs.";
                    break;
                case 150:
                    tempTime = "2.5 hrs.";
                    break;
                case 180:
                    tempTime = "3 hrs.";
                    break;
            }
            this.tempAdvance = this.feature.Costo_Cita;
            this.HeightWidth = $"{Languages.MaximunSize}: {this.feature.Alto} cm X {this.feature.Ancho} cm";
            this.AppCost = $"{Languages.Cost}: {this.feature.Total_Aprox.ToString("C2")}";
            this.AppAdvance = $"{Languages.Advance}: {this.feature.Costo_Cita.ToString("C2")}";
            this.AppDuration = $"{Languages.EstimatedTime} {tempTime}";
        }
        public void LoadCitas()
        {
            var color = Color.Black;
            int red = (int)(color.R * 255);
            int green = (int)(color.G * 255);
            int blue = (int)(color.B * 255);
            string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

            if (changeDate == true || addNewDate == true)
            {
                    this.CitaList = MainViewModel.GetInstance().TecnicoHome.CitasList.Select(c => new CitasItemViewModel
                    {
                        Id_Cita = c.Id_Cita,
                        Id_Trabajo = c.Id_Trabajo,
                        Id_Cliente = c.Id_Cliente,
                        Id_Tatuador = c.Id_Tatuador,
                        F_Inicio = new DateTime(c.F_Inicio.Year, c.F_Inicio.Month, c.F_Inicio.Day, c.H_Inicio.Hours, c.H_Inicio.Minutes, c.H_Inicio.Seconds),
                        H_Inicio = c.H_Inicio,
                        F_Fin = new DateTime(c.F_Fin.Year, c.F_Fin.Month, c.F_Fin.Day, c.H_Fin.Hours, c.H_Fin.Minutes, c.H_Fin.Seconds),
                        H_Fin = c.H_Fin,
                        Asunto = 
                        (
                        c.CambioFecha == true || c.TecnicoTiempo == true ?
                            "":
                            c.Asunto
                        ),

                        Completa = c.Completa,

                        ColorText =
                        (
                        c.CambioFecha == true || c.TecnicoTiempo == true ?
                            hex:
                            c.ColorText
                        ),
                        Color =
                        (
                        c.CambioFecha == true || c.TecnicoTiempo == true ?
                            Color.FromHex(hex):
                            Color.FromHex(c.ColorText)
                        ),
                        Completado =
                        (
                            MainViewModel.GetInstance().TecnicoHome.TrabajoList.Any(t => t.Id_Trabajo == c.Id_Trabajo) ?
                            MainViewModel.GetInstance().TecnicoHome.TrabajoList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Completo:
                            false
                        ),
                        Cancelado =
                        (
                            MainViewModel.GetInstance().TecnicoHome.TrabajoList.Any(t => t.Id_Trabajo == c.Id_Trabajo) ?
                            MainViewModel.GetInstance().TecnicoHome.TrabajoList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Cancelado :
                            false
                        ),
                        TecnicoTiempo = c.TecnicoTiempo,
                        CambioFecha = c.CambioFecha,
                        CitaTemp = c.CitaTemp,
                        Pagado =
                        (
                            !MainViewModel.GetInstance().Login.ListPagosCliente.Any(p => p.Id_Trabajo == c.Id_Trabajo) ?
                            true :
                            MainViewModel.GetInstance().Login.ListPagosCliente.FirstOrDefault(p => p.Id_Trabajo == c.Id_Trabajo).Pagado
                        ),
                    }).Where(c => c.Completa == false && c.Cancelado == false && c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();
            }
            else
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
                        Asunto =
                        (
                        c.CambioFecha == true || c.TecnicoTiempo == true ?
                            "" :
                            c.Asunto
                        ),

                        Completa = c.Completa,

                        ColorText =
                        (
                        c.CambioFecha == true || c.TecnicoTiempo == true ?
                            hex :
                            c.ColorText
                        ),
                        Color =
                        (
                        c.CambioFecha == true || c.TecnicoTiempo == true ?
                            Color.FromHex(hex) :
                            Color.FromHex(c.ColorText)
                        ),
                        TecnicoTiempo = c.TecnicoTiempo,
                        CambioFecha = c.CambioFecha,
                        CitaTemp = c.CitaTemp,
                        Completado =
                        (
                            MainViewModel.GetInstance().UserHome.TrabajosList.Any(t => t.Id_Trabajo == c.Id_Trabajo) ?
                            MainViewModel.GetInstance().UserHome.TrabajosList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Completo :
                            false
                        ),
                        Cancelado =
                        (
                            MainViewModel.GetInstance().UserHome.TrabajosList.Any(t => t.Id_Trabajo == c.Id_Trabajo) ?
                            MainViewModel.GetInstance().UserHome.TrabajosList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Cancelado :
                            false
                        ),
                        Pagado =
                        (
                            !MainViewModel.GetInstance().Login.ListPagosCliente.Any(p => p.Id_Trabajo == c.Id_Trabajo) ?
                            true :
                            MainViewModel.GetInstance().Login.ListPagosCliente.FirstOrDefault(p => p.Id_Trabajo == c.Id_Trabajo).Pagado
                        ),

                    }).Where(c => c.Completa == false && c.Cancelado == false && c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();
            }
            if (this.newCita != null)
            {
                this.CitaList.Add(this.newCita);
            }
            this.Citas = new ObservableCollection<CitasItemViewModel>(this.CitaList);
            
        }
        private async void NewDateSelected()
        {

            this.newCita = null;
            this.LoadCitas();

            var times = (feature.Tiempo / 30) - 1;
            var TempDate = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);
            var tempTime = new TimeSpan(TempDate.Hour, TempDate.Minute, TempDate.Second);
            var tempStartDate = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, 0, 0, 0);
            var tempEndDate = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, 23, 59, 59);
            var tempCitaList = this.CitaList.Where(c => c.F_Inicio.Ticks > tempStartDate.Ticks && c.F_Inicio.Ticks < tempEndDate.Ticks).ToList();
            var lastCita = new T_trabajocitas();
            if (tempCitaList.Count > 0)
            {
                for (int i = 0; i <= times; i++)
                {
                    foreach (var cita in tempCitaList)
                    {
                        if (tempTime.Ticks >= cita.H_Inicio.Ticks && tempTime.Ticks < cita.H_Fin.Ticks)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                                Languages.Error,
                                Languages.ScheduleError,
                                "OK");
                                this.newCita = null;
                                this.LoadCitas();
                            return;
                        }
                    }
                    if (tempTime.Ticks < this.workIn.Ticks || tempTime.Ticks >= this.workOut.Ticks)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                                Languages.Error,
                                Languages.ScheduleError,
                                "OK");
                            this.newCita = null;
                            this.LoadCitas();
                        return;
                    }
                    if (tempTime.Ticks >= this.eatOut.Ticks && tempTime.Ticks < this.eatIn.Ticks)
                    {
                            await Application.Current.MainPage.DisplayAlert(
                                Languages.Error,
                                Languages.ScheduleError,
                                "OK");
                            this.newCita = null;
                            this.LoadCitas();
                        return;
                    }

                        TempDate = TempDate.AddMinutes(30.00);
                        tempTime = new TimeSpan(TempDate.Hour, TempDate.Minute, TempDate.Second);
                    
                }
            }
            else if(tempCitaList.Count <= 0)
            {
                for (int i = 0; i <= times; i++)
                {
                    if (tempTime.Ticks < this.workIn.Ticks || tempTime.Ticks >= this.workOut.Ticks)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.ScheduleError,
                            "OK");
                            this.newCita = null;
                            this.LoadCitas();
                        return;
                    }
                    if (tempTime.Ticks >= this.eatOut.Ticks && tempTime.Ticks < this.eatIn.Ticks)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.ScheduleError,
                            "OK");
                            this.newCita = null;
                            this.LoadCitas();
                        return;
                    }

                    TempDate = TempDate.AddMinutes(30.00);
                    tempTime = new TimeSpan(TempDate.Hour, TempDate.Minute, TempDate.Second);
                }
            }            

            var newEndDate = SelectedTime.AddMinutes(this.feature.Tiempo);
            var color = Color.Green;
            int red = (int)(color.R * 255);
            int green = (int)(color.G * 255);
            int blue = (int)(color.B * 255);
            string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);

            if(this.PresupuestoPage == true)
            {
                this.subject = $"{Languages.Tattoo} {Languages.Personalized}, {this.feature.Alto} cm X {this.feature.Ancho} cm.";
            }
            else
            {
                if(this.changeDate == false || addNewDate == true)
                {
                    this.subject = $"{Languages.Tattoo}, {this.feature.Alto} cm X {this.feature.Ancho} cm, {Languages.Complexity} {this.complexity}.";
                }
                else
                {
                    this.subject = $"{Languages.Tattoo}, {this.feature.Alto} cm X {this.feature.Ancho} cm.";
                }
            }

            this.newCita = new CitasItemViewModel
            {
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                F_Inicio = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                H_Inicio = new TimeSpan(SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                F_Fin = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                H_Fin = new TimeSpan(newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                Asunto = this.subject,
                Completa = false,
                ColorText = hex,
                Color = Color.FromHex(hex),
                Completado = false,
                Cancelado = false,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = false,
                Pagado = true,

            };
            this.hourStart = SelectedTime.ToString("h:mm tt");
            this.dateStart = SelectedTime.ToString("dd MMM yyyy");
            this.LoadCitas();
        }
        public void LoadHorarios()
        {
            var horario = new T_tecnicohorarios();

            if(changeDate == true || addNewDate == true)
            {
                horario = MainViewModel.GetInstance().TecnicoHome.horario;
            }
            else
            {
                horario = MainViewModel.GetInstance().UserHome.ListHorariosTecnicos.Single(h => h.Id_Tecnico == this.tecnico.Id_Tecnico);
            }
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

                    if (horario.Hlvcomidaact == true)
                    {
                        this.EatInTime = $"{horario.Hlvcomidade.Hours.ToString()}";
                        this.eatIn = new TimeSpan(horario.Hlvcomidade.Hours, horario.Hlvcomidade.Minutes, 0);
                        this.EatOutTime = $"{horario.Hlvcomidaa.Hours.ToString()}";
                        this.eatOut = new TimeSpan(horario.Hlvcomidaa.Hours, horario.Hlvcomidaa.Minutes, 0);
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

                    if (horario.Hscomidaact == true)
                    {
                        this.EatInTime = $"{horario.Hscomidade.Hours.ToString()}";
                        this.eatIn = new TimeSpan(horario.Hscomidade.Hours, horario.Hscomidade.Minutes, 0);
                        this.EatOutTime = $"{horario.Hscomidaa.Hours.ToString()}";
                        this.eatOut = new TimeSpan(horario.Hscomidaa.Hours, horario.Hscomidaa.Minutes, 0);
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

                    if (horario.Hdcomidaact == true)
                    {
                        this.EatInTime = $"{horario.Hdcomidade.Hours.ToString()}";
                        this.eatIn = new TimeSpan(horario.Hdcomidade.Hours, horario.Hdcomidade.Minutes, 0);
                        this.EatOutTime = $"{horario.Hdcomidaa.Hours.ToString()}";
                        this.eatOut = new TimeSpan(horario.Hdcomidaa.Hours, horario.Hdcomidaa.Minutes, 0);
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
        private void LoadInfoLocal()
        {
            this.ciudad = MainViewModel.GetInstance().UserHome.CiudadesList.Single(c => c.Id == this.local.Id_Ciudad);
            this.estado = MainViewModel.GetInstance().UserHome.EstadosList.Single(e => e.Id == this.local.Id_Estado);

            if (MainViewModel.GetInstance().Login.ListBalanceCliente.Any(b => b.Id_Usuario == this.cliente.Id_Usuario))
            {
                var balance = MainViewModel.GetInstance().Login.ListBalanceCliente.FirstOrDefault(b => b.Id_Usuario == this.cliente.Id_Usuario);
                if (balance.Saldo_Favor >= this.tempAdvance)
                {
                    this.newSaldoFavor = balance.Saldo_Favor - this.tempAdvance;
                    this.AppAdvance = $"{Languages.Advance}: {0.ToString("C2")}";
                    this.SaldoFavorPayMethod = true;
                }
            }
            this.reference = $"{Languages.Reference} {this.local.Referencia}";
            this.studio = $"{this.empresa.Nombre} {Languages.BranchOffice} {this.local.Nombre}";
            this.address = $"{this.local.Calle} {this.local.Numero}, {this.postal.Asentamiento} {this.postal.Colonia}, " +
                $"C.P. {this.postal.Id.ToString()}, {this.ciudad.Ciudad}, {this.estado.Estado}.";
            this.artist = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
            this.comision = ((this.tempAdvance * new decimal(4)) / new decimal(100)) + new decimal(4);
            this.PayPalComission = $"{Languages.PaypalComission}: {comision}";
        }
        private async void LoadPostal()
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
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();

            //-----------------Cargar Datos Postal-----------------//

            var controller = Application.Current.Resources["UrlT_postalController"].ToString();

            var response = await this.apiService.Get<T_postal>(urlApi, prefix, controller, this.local.Id_Colonia);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.postal = (T_postal)response.Result;
        }

        private async void SavePersonal()
        {
            this.apiService.StartActivityPopup();
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

             var trabajotemp = new T_trabajostemp
            {
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                Asunto = subject,
                Alto = decimal.Parse(this.Height),
                Ancho = decimal.Parse(this.Width),
                Total_Aprox = 0,
                Costo_Cita = 0,
                Tiempo = 0,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajostempController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, trabajotemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            trabajotemp = (T_trabajostemp)response.Result;

            var notatemp = new T_trabajonotatemp
            {
                Id_Trabajotemp = trabajotemp.Id_Trabajotemp,
                Tipo_Usuario = 1,
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Local = this.tecnico.Id_Local,
                Nota = this.DescribeArt,
                F_nota = this.SelectedTime,
                Nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}",
                Propuesta = false,
            };

            controller = Application.Current.Resources["UrlT_trabajonotatempController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, notatemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            notatemp = (T_trabajonotatemp)response.Result;

            var notaImagentemp = new T_citaimagenestemp
            {
                Imagen = FileHelper.ReadFully(this.file.GetStream()),
                Id_Trabajotemp = trabajotemp.Id_Trabajotemp,

            };
            controller = Application.Current.Resources["UrlT_citaimagenestempController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, notaImagentemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            this.apiService.EndActivityPopup();

            var fromName = $"{this.cliente.Nombre} {this.cliente.Apellido}";
            var To = this.tecnico.Id_Usuario;
            var notif = $"{Languages.TheClient} {fromName} {Languages.NotifNewPersonalizedApp}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.cliente.Id_Usuario,
                Usuario_Recibe = this.tecnico.Id_Usuario,
                Notificacion = notif,
                Fecha = DateTime.Now.ToLocalTime(),
                Visto = false,
            };
            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response.Result;
            //TipoNotif Cita =1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_TrabajoTemp = trabajotemp.Id_Trabajotemp,
                TipoNotif = 2,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var message = $"{Languages.TempJobMessageSent} {this.tecnico.Nombre} '{this.tecnico.Apodo}' {this.tecnico.Apellido}.{'\n'}{'\n'} {Languages.TempJobAnswerMessage}.";

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                message,
                "Ok"
                );
        }
        private async void SaveQuickDate()
        {
            this.apiService.StartActivityPopup();

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var newTrabajo = new T_trabajos
            {
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                Asunto = this.subject,
                Id_Caract = this.feature.Id_Caract,
                Total_Aprox = this.feature.Total_Aprox,
                Costo_Cita = this.feature.Costo_Cita,
                Alto = this.feature.Alto,
                Ancho = this.feature.Ancho,
                Tiempo = this.feature.Tiempo,
                Completo = false,
                Cancelado = false,
                Trabajo_Iniciado = false,
                TecnicoTiempo = false,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, newTrabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            this.trabajo = (T_trabajos)response.Result;
            MainViewModel.GetInstance().UserHome.TrabajosList.Add(this.trabajo);

            var color = Color.DarkRed;
            int red = (int)(color.R * 255);
            int green = (int)(color.G * 255);
            int blue = (int)(color.B * 255);
            string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);
            var newEndDate = SelectedTime.AddMinutes(this.feature.Tiempo);

            var cita = new T_trabajocitas
            {
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                F_Inicio = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                H_Inicio = new TimeSpan(SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                F_Fin = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                H_Fin = new TimeSpan(newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                Asunto = this.subject,
                Completa = false,
                ColorText = hex,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = false,
            };

            controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            cita = (T_trabajocitas)response.Result;

            var nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}";
            var nota = new T_trabajonota
            {
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Tipo_Usuario = 1,
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Local = this.tecnico.Id_Local,
                Id_Cita = cita.Id_Cita,
                Nota = this.DescribeArt,
                Nombre_Post = nombre_Post,
                F_nota = DateTime.Now.ToLocalTime(),
                Cambio_Fecha = false,
            };

            controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, nota);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            nota = (T_trabajonota)response.Result;
            
            var notaImagen = new T_citaimagenes
            {
                Imagen = FileHelper.ReadFully(this.file.GetStream()),
                Id_Trabajo = this.trabajo.Id_Trabajo,

            };
            

            controller = Application.Current.Resources["UrlT_citaimagenesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, notaImagen);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            // Tipo_Pago 1 = Efectivo
            // Tipo_Pago 2 = PayPal
            // Tipo_Pago 3 = Tarjeta
            // Tipo_Pago 4 = Saldo Favor
            var pago = new T_pagoscliente
            {
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Pago = this.trabajo.Costo_Cita,
                Tipo_Pago =
                (
                    this.CashMetodoChecked == true ?
                    1 :
                    this.SaldoFavorPayMethod == true ?
                    4 :
                    2
                ),
                Pagado =
                (
                    this.CashMetodoChecked == true && this.SaldoFavorPayMethod == false ?
                    false :
                    this.CashMetodoChecked == true && this.SaldoFavorPayMethod == true ?
                    true :
                    true
                ),
                Fecha_Pago = DateTime.Now,
                Fecha_Peticion = DateTime.Now,

            };
            controller = Application.Current.Resources["UrlT_pagosclienteController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, pago);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            pago = (T_pagoscliente)response.Result;
            if(SaldoFavorPayMethod == true)
            {
                var saldo = MainViewModel.GetInstance().Login.ListBalanceCliente.FirstOrDefault(b => b.Id_Usuario == this.cliente.Id_Usuario);
                var newSaldo = new T_balancecliente
                {
                    Id_Balancecliente = saldo.Id_Balancecliente,
                    Id_Cliente= saldo.Id_Cliente,
                    Id_Usuario = saldo.Id_Usuario,
                    Saldo_Favor = this.newSaldoFavor,
                };

                controller = Application.Current.Resources["UrlT_balanceclienteController"].ToString();

                response = await this.apiService.Put(urlApi, prefix, controller, newSaldo, saldo.Id_Balancecliente);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }
                newSaldo = (T_balancecliente)response.Result;
                var oldSaldo = MainViewModel.GetInstance().Login.ListBalanceCliente.Where(p => p.Id_Balancecliente == saldo.Id_Balancecliente).FirstOrDefault();
                if (oldSaldo != null)
                {
                    MainViewModel.GetInstance().Login.ListBalanceCliente.Remove(oldSaldo);
                }
                MainViewModel.GetInstance().Login.ListBalanceCliente.Add(newSaldo);

            }
            MainViewModel.GetInstance().Login.ListPagosCliente.Add(pago);
            if (CashMetodoChecked != true || (CashMetodoChecked == true && SaldoFavorPayMethod == true))
            {
                if (!MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Tecnico == this.tecnico.Id_Tecnico))
                {
                    var newSaldo = new T_balancetecnico
                    {
                        Id_Tecnico = this.tecnico.Id_Tecnico,
                        Id_Usuario = this.tecnico.Id_Usuario,
                        Saldo_Contra = 0,
                        Saldo_Favor = 0,
                        Saldo_Retenido = pago.Pago - 50,
                    };

                    controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                    response = await this.apiService.Post(urlApi, prefix, controller, newSaldo);

                    if (!response.IsSuccess)
                    {
                        this.apiService.EndActivityPopup();

                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                        return;
                    }
                    newSaldo = (T_balancetecnico)response.Result;

                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newSaldo);
                }
                else
                {
                    var saldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Tecnico == this.trabajo.Id_Tatuador);
                    var newSaldo = new T_balancetecnico
                    {
                        Id_Balancetecnico = saldo.Id_Balancetecnico,
                        Id_Tecnico = saldo.Id_Tecnico,
                        Id_Usuario = saldo.Id_Usuario,
                        Saldo_Contra = saldo.Saldo_Contra,
                        Saldo_Favor = saldo.Saldo_Favor,
                        Saldo_Retenido = saldo.Saldo_Retenido + (pago.Pago - 50),
                    };

                    controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                    response = await this.apiService.Put(urlApi, prefix, controller, newSaldo, saldo.Id_Balancetecnico);

                    if (!response.IsSuccess)
                    {
                        this.apiService.EndActivityPopup();

                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                        return;
                    }
                    newSaldo = (T_balancetecnico)response.Result;
                    var oldSaldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.Where(p => p.Id_Balancetecnico == saldo.Id_Balancetecnico).FirstOrDefault();
                    if (oldSaldo != null)
                    {
                        MainViewModel.GetInstance().Login.ListBalanceTecnico.Remove(oldSaldo);
                    }
                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newSaldo);
                }
            }

            MainViewModel.GetInstance().UserHome.LoadCliente();
            MainViewModel.GetInstance().UserPage.LoadUser();

            MainViewModel.GetInstance().UserHome.CitaList.Add(cita);
            MainViewModel.GetInstance().UserHome.RefreshCitaList();

            this.apiService.EndActivityPopup();

            string date = DateTime.Parse(cita.F_Inicio.ToString()).ToString("dd-MMM-yyyy");
            string time = DateTime.Parse(cita.H_Inicio.ToString()).ToString("hh:mm tt");


            var message = $"{Languages.YourAppointment} #{this.trabajo.Id_Trabajo} {Languages.HasBeenCreated}  {date}  {Languages.At}  {time} " +
                $" {Languages.WithThe_Artist}  {this.tecnico.Nombre} '{this.tecnico.Apodo}' {this.tecnico.Apellido}.{'\n'}{'\n'} {Languages.TryToBe}.";

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                message,
                "Ok"
                );

            var fromName = $"{this.cliente.Nombre} {this.cliente.Apellido}";
            var To = this.tecnico.Id_Usuario;
            var notif = $"{Languages.TheClient} {fromName} {Languages.NotifNewQuickApp} {date} {Languages.At} {time}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.cliente.Id_Usuario,
                Usuario_Recibe = this.tecnico.Id_Usuario,
                Notificacion = notif,
                Fecha = DateTime.Now.ToLocalTime(),
                Visto = false,

            };
            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response.Result;

            //TipoNotif Cita =1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_Cita = cita.Id_Cita,
                TipoNotif = 1,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
        }
        private async void SavePersonalDate()
        {
            this.apiService.StartActivityPopup();
            var trabajoTemp = MainViewModel.GetInstance().UserMessageJob.Trabajo;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var newTrabajo = new T_trabajos
            {
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                Asunto = this.subject,
                Id_Caract = null,
                Total_Aprox = trabajoTemp.Total_Aprox,
                Costo_Cita = trabajoTemp.Costo_Cita,
                Alto = trabajoTemp.Alto,
                Ancho = trabajoTemp.Ancho,
                Tiempo = trabajoTemp.Tiempo,
                Completo = false,
                Cancelado = false,
                Trabajo_Iniciado = false,
                TecnicoTiempo = false,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, newTrabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            this.trabajo = (T_trabajos)response.Result;

            MainViewModel.GetInstance().UserHome.TrabajosList.Add(this.trabajo);

            var color = Color.DarkBlue;
            int red = (int)(color.R * 255);
            int green = (int)(color.G * 255);
            int blue = (int)(color.B * 255);
            string hex = String.Format("#{0:X2}{1:X2}{2:X2}", red, green, blue);
            var newEndDate = SelectedTime.AddMinutes(this.feature.Tiempo);

            var cita = new T_trabajocitas
            {
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                F_Inicio = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                H_Inicio = new TimeSpan(SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                F_Fin = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                H_Fin = new TimeSpan(newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                Asunto = this.subject,
                Completa = false,
                ColorText = hex,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = false,
            };

            controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            cita = (T_trabajocitas)response.Result;

            this.TrabajoNotaList = MainViewModel.GetInstance().UserMessageJob.TrabajoNotaListtemp;
            var budgetNote = this.TrabajoNotaList.Where(n => n.Id_Notatemp == MainViewModel.GetInstance().UserMessageJob.notaSelected.Id_Notatemp).FirstOrDefault();
            if(budgetNote != null)
            {
                this.TrabajoNotaList.Remove(budgetNote);
            }

            foreach(var notaTemp in this.TrabajoNotaList)
            {
                var nota = new T_trabajonota
                {
                    Id_Trabajo = this.trabajo.Id_Trabajo,
                    Tipo_Usuario = notaTemp.Tipo_Usuario,
                    Id_Usuario = notaTemp.Id_Usuario,
                    Id_Local = notaTemp.Id_Local,
                    Id_Cita = cita.Id_Cita,
                    Nota = notaTemp.Nota,
                    Nombre_Post = notaTemp.Nombre_Post,
                    F_nota = notaTemp.F_nota,
                    Cambio_Fecha = false,
                };

                controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

                response = await this.apiService.Post(urlApi, prefix, controller, nota);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }
            }
            this.TrabajoNotaList.Add(budgetNote);
            var notaImagen = new T_citaimagenes
            {
                Imagen = trabajoTemp.Imagen,
                Id_Trabajo = this.trabajo.Id_Trabajo,
            };
            controller = Application.Current.Resources["UrlT_citaimagenesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, notaImagen);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            // Tipo_Pago 1 = Efectivo
            // Tipo_Pago 2 = PayPal
            // Tipo_Pago 3 = Tarjeta
            // Tipo_Pago 4 = Saldo Favor
            var pago = new T_pagoscliente
            {
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Pago = this.trabajo.Costo_Cita,
                Tipo_Pago =
                (
                    this.CashMetodoChecked == true ?
                    1 :
                    this.SaldoFavorPayMethod == true ?
                    4 :
                    2
                ),
                Pagado =
                (
                    this.CashMetodoChecked == true && this.SaldoFavorPayMethod == false ?
                    false :
                    this.CashMetodoChecked == true && this.SaldoFavorPayMethod == true ?
                    true :
                    true
                ),
                Fecha_Pago = DateTime.Now,
                Fecha_Peticion = DateTime.Now,

            };
            controller = Application.Current.Resources["UrlT_pagosclienteController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, pago);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            pago = (T_pagoscliente)response.Result;

            MainViewModel.GetInstance().Login.ListPagosCliente.Add(pago);

            if (SaldoFavorPayMethod == true)
            {
                var saldo = MainViewModel.GetInstance().Login.ListBalanceCliente.FirstOrDefault(b => b.Id_Usuario == this.cliente.Id_Usuario);
                var newSaldo = new T_balancecliente
                {
                    Id_Balancecliente = saldo.Id_Balancecliente,
                    Id_Cliente = saldo.Id_Cliente,
                    Id_Usuario = saldo.Id_Usuario,
                    Saldo_Favor = this.newSaldoFavor,
                };

                controller = Application.Current.Resources["UrlT_balanceclienteController"].ToString();

                response = await this.apiService.Put(urlApi, prefix, controller, newSaldo, saldo.Id_Balancecliente);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }
                newSaldo = (T_balancecliente)response.Result;
                var oldSaldo = MainViewModel.GetInstance().Login.ListBalanceCliente.Where(p => p.Id_Balancecliente == saldo.Id_Balancecliente).FirstOrDefault();
                if (oldSaldo != null)
                {
                    MainViewModel.GetInstance().Login.ListBalanceCliente.Remove(oldSaldo);
                }
                MainViewModel.GetInstance().Login.ListBalanceCliente.Add(newSaldo);

            }

            if (CashMetodoChecked != true || (CashMetodoChecked == true && SaldoFavorPayMethod == true))
            {
                if (!MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Tecnico == this.tecnico.Id_Tecnico))
                {
                    var newSaldo = new T_balancetecnico
                    {
                        Id_Tecnico = this.tecnico.Id_Tecnico,
                        Id_Usuario = this.tecnico.Id_Usuario,
                        Saldo_Contra = 0,
                        Saldo_Favor = 0,
                        Saldo_Retenido = pago.Pago - 50,
                    };

                    controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                    response = await this.apiService.Post(urlApi, prefix, controller, newSaldo);

                    if (!response.IsSuccess)
                    {
                        this.apiService.EndActivityPopup();

                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                        return;
                    }
                    newSaldo = (T_balancetecnico)response.Result;

                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newSaldo);
                }
                else
                {
                    var saldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Tecnico == this.trabajo.Id_Tatuador);
                    var newSaldo = new T_balancetecnico
                    {
                        Id_Balancetecnico = saldo.Id_Balancetecnico,
                        Id_Tecnico = saldo.Id_Tecnico,
                        Id_Usuario = saldo.Id_Usuario,
                        Saldo_Contra = saldo.Saldo_Contra,
                        Saldo_Favor = saldo.Saldo_Favor,
                        Saldo_Retenido = saldo.Saldo_Retenido + (pago.Pago - 50),
                    };

                    controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                    response = await this.apiService.Put(urlApi, prefix, controller, newSaldo, saldo.Id_Balancetecnico);

                    if (!response.IsSuccess)
                    {
                        this.apiService.EndActivityPopup();

                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                        return;
                    }
                    newSaldo = (T_balancetecnico)response.Result;
                    var oldSaldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.Where(p => p.Id_Balancetecnico == saldo.Id_Balancetecnico).FirstOrDefault();
                    if (oldSaldo != null)
                    {
                        MainViewModel.GetInstance().Login.ListBalanceTecnico.Remove(oldSaldo);
                    }
                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newSaldo);
                }
            }

            MainViewModel.GetInstance().UserHome.LoadCliente();
            var oldTrabajo = MainViewModel.GetInstance().UserMessages.TrabajosList.Where(t => t.Id_Trabajotemp == trabajoTemp.Id_Trabajotemp).FirstOrDefault();

            controller = Application.Current.Resources["UrlT_trabajostempController"].ToString();

            response = await this.apiService.Delete(urlApi, prefix, controller, oldTrabajo.Id_Trabajotemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            if (oldTrabajo != null)
            {
                MainViewModel.GetInstance().UserMessages.TrabajosList.Remove(oldTrabajo);
            }

            foreach (var notatemp in this.TrabajoNotaList)
            {
                var oldNotatemp = MainViewModel.GetInstance().UserMessages.TrabajoNotaList.Where(t => t.Id_Notatemp == notatemp.Id_Notatemp).FirstOrDefault();
                controller = Application.Current.Resources["UrlT_trabajonotatempController"].ToString();

                response = await this.apiService.Delete(urlApi, prefix, controller, oldNotatemp.Id_Notatemp);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();
                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }
                if (oldNotatemp != null)
                {
                    MainViewModel.GetInstance().UserMessages.TrabajoNotaList.Remove(oldNotatemp);
                }

            }
            var oldNotaImagentemp = MainViewModel.GetInstance().UserMessages.ImagesList.Where(t => t.Id_Trabajotemp == trabajoTemp.Id_Trabajotemp).FirstOrDefault();
            
            controller = Application.Current.Resources["UrlT_citaimagenestempController"].ToString();

            response = await this.apiService.Delete(urlApi, prefix, controller, oldNotaImagentemp.Id_Imagentemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            if (oldNotaImagentemp != null)
            {
                MainViewModel.GetInstance().UserMessages.ImagesList.Remove(oldNotaImagentemp);
            }

            if (newCita != null)
            {
                MainViewModel.GetInstance().UserHome.CitaList.Remove(this.newCita);
                this.newCita = null;
                this.LoadCitas();
            }

            MainViewModel.GetInstance().UserHome.LoadCliente();
            MainViewModel.GetInstance().UserPage.LoadUser();
            MainViewModel.GetInstance().UserHome.CitaList.Add(cita);
            MainViewModel.GetInstance().UserHome.RefreshCitaList();

            string date = DateTime.Parse(cita.F_Inicio.ToString()).ToString("dd-MMM-yyyy");
            string time = DateTime.Parse(cita.H_Inicio.ToString()).ToString("hh:mm tt");

            var fromName = $"{this.cliente.Nombre} {this.cliente.Apellido}";
            var To = this.tecnico.Id_Usuario;
            var notif = $"{Languages.TheClient} {fromName} {Languages.NotifAcceptBudget} {date} {Languages.At} {time}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.cliente.Id_Usuario,
                Usuario_Recibe = this.tecnico.Id_Usuario,
                Notificacion = notif,
                Fecha = DateTime.Now.ToLocalTime(),
                Visto = false,
            };
            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response.Result;

            //TipoNotif Cita =1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_Cita = cita.Id_Cita,
                TipoNotif = 1,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var message = $"{Languages.YourAppointment} #{this.trabajo.Id_Trabajo} {Languages.HasBeenCreated}  {date}  {Languages.At}  {time} " +
                $" {Languages.WithThe_Artist}  {this.tecnico.Nombre} '{this.tecnico.Apodo}' {this.tecnico.Apellido}.{'\n'}{'\n'} {Languages.TryToBe}.";

            this.apiService.EndActivityPopup();

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                message,
                "Ok"
                );

            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void ChangeDate()
        {
            this.apiService.StartActivityPopup();

            var cita = MainViewModel.GetInstance().TecnicoViewDate.cita;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var newEndDate = SelectedTime.AddMinutes(this.feature.Tiempo);
            var newCitaTemp = new T_trabajocitas
            {
                Id_Trabajo = cita.Id_Trabajo,
                Id_Cliente = cita.Id_Cliente,
                Id_Tatuador = cita.Id_Tatuador,
                F_Inicio = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                H_Inicio = new TimeSpan(SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                F_Fin = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                H_Fin = new TimeSpan(newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                Asunto = cita.Asunto,
                Completa = cita.Completa,
                ColorText = cita.ColorText,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = true,
            };

            var controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, newCitaTemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newCitaTemp = (T_trabajocitas)response.Result;

            MainViewModel.GetInstance().TecnicoHome.CitasList.Add(newCitaTemp);

            var newOldCita = new T_trabajocitas
            {
                Id_Cita = cita.Id_Cita,
                Id_Trabajo = cita.Id_Trabajo,
                Id_Cliente = cita.Id_Cliente,
                Id_Tatuador = cita.Id_Tatuador,
                F_Inicio = cita.F_Inicio,
                H_Inicio = cita.H_Inicio,
                F_Fin = cita.F_Fin,
                H_Fin = cita.H_Fin,
                Asunto = cita.Asunto,
                Completa = cita.Completa,
                ColorText = cita.ColorText,
                CambioFecha = true,
                TecnicoTiempo = false,
                CitaTemp = false,
            };
            response = await this.apiService.Put(urlApi, prefix, controller, newOldCita, cita.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newCitaTemp = (T_trabajocitas)response.Result;

            var oldCitaTemp = MainViewModel.GetInstance().TecnicoHome.CitasList.Where(n => n.Id_Cita == cita.Id_Cita).FirstOrDefault();
            if (oldCitaTemp != null)
            {
                MainViewModel.GetInstance().TecnicoHome.CitasList.Remove(oldCitaTemp);
            }
            
            MainViewModel.GetInstance().TecnicoHome.CitasList.Add(newCitaTemp);
            MainViewModel.GetInstance().TecnicoViewJob.RerfeshCitasList();

            if (newCita != null)
            {
                MainViewModel.GetInstance().TecnicoHome.CitasList.Remove(this.newCita);
                this.newCita = null;
                this.LoadCitas();
            }

            this.apiService.EndActivityPopup();

            string date = DateTime.Parse(SelectedDate.ToString()).ToString("dd-MMM-yyyy").ToUpper();
            string time = DateTime.Parse(SelectedDate.ToString()).ToString("hh:mm tt").ToUpper();

            var message = $"{Languages.TheDateChanged} #{cita.Id_Cita} {Languages.WillBeChanged}  {date}  {Languages.To}  {time}  {Languages.CustomerAccept}.";

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                message,
                "Ok"
                );

            MainViewModel.GetInstance().TecnicoViewDate.SendMessage();
            
            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PopModalAsync();

            this.cliente = MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault(c => c.Id_Cliente == cita.Id_Cliente);
            var fromName = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
            var To = this.cliente.Id_Usuario;
            var notif = $"{Languages.TheArtist} {fromName} {Languages.NotifChageDate} # {cita.Id_Cita}: {cita.Asunto}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.tecnico.Id_Usuario,
                Usuario_Recibe = this.cliente.Id_Usuario,
                Notificacion = notif,
                Fecha = DateTime.Now.ToLocalTime(),
                Visto = false,
            };
            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response.Result;

            //TipoNotif Cita =1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_Cita = cita.Id_Cita,
                TipoNotif = 1,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
        }
        private async void AddNewDate()
        {
            this.apiService.StartActivityPopup();

            var cita = MainViewModel.GetInstance().TecnicoViewDate.cita;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            var newEndDate = SelectedTime.AddMinutes(this.feature.Tiempo);
            var newCitaTemp = new T_trabajocitas
            {
                Id_Trabajo = cita.Id_Trabajo,
                Id_Cliente = cita.Id_Cliente,
                Id_Tatuador = cita.Id_Tatuador,
                F_Inicio = new DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day, SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                H_Inicio = new TimeSpan(SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second),
                F_Fin = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                H_Fin = new TimeSpan(newEndDate.Hour, newEndDate.Minute, newEndDate.Second),
                Asunto = cita.Asunto,
                Completa = false,
                ColorText = cita.ColorText,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = false,
            };

            var controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, newCitaTemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            newCitaTemp = (T_trabajocitas)response.Result;

            MainViewModel.GetInstance().TecnicoHome.CitasList.Add(newCitaTemp);

            var newOldCita = new T_trabajocitas
            {
                Id_Cita = cita.Id_Cita,
                Id_Trabajo = cita.Id_Trabajo,
                Id_Cliente = cita.Id_Cliente,
                Id_Tatuador = cita.Id_Tatuador,
                F_Inicio = cita.F_Inicio,
                H_Inicio = cita.H_Inicio,
                F_Fin = cita.F_Fin,
                H_Fin = cita.H_Fin,
                Asunto = cita.Asunto,
                Completa = true,
                ColorText = cita.ColorText,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = false,
            };
            response = await this.apiService.Put(urlApi, prefix, controller, newOldCita, cita.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newOldCita = (T_trabajocitas)response.Result;

            var oldCitaTemp = MainViewModel.GetInstance().TecnicoHome.CitasList.Where(n => n.Id_Cita == cita.Id_Cita).FirstOrDefault();
            if (oldCitaTemp != null)
            {
                MainViewModel.GetInstance().TecnicoHome.CitasList.Remove(oldCitaTemp);
            }

            MainViewModel.GetInstance().TecnicoHome.CitasList.Add(newOldCita);
            MainViewModel.GetInstance().TecnicoViewJob.RerfeshCitasList();

            this.apiService.EndActivityPopup();

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                Languages.AddNewDateMessage,
                "Ok"
                );

            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PopModalAsync();

            string date = DateTime.Parse(newCitaTemp.F_Inicio.ToString()).ToString("dd-MMM-yyyy");
            string time = DateTime.Parse(newCitaTemp.H_Inicio.ToString()).ToString("hh:mm tt");

            this.cliente = MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault(c => c.Id_Cliente == cita.Id_Cliente);
            var fromName = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
            var To = this.cliente.Id_Usuario;
            var notif = $"{Languages.TheArtist} {fromName} {Languages.NotifNewApp} {date} {Languages.At} {time}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.tecnico.Id_Usuario,
                Usuario_Recibe = this.cliente.Id_Usuario,
                Notificacion = notif,
                Fecha = DateTime.Now.ToLocalTime(),
                Visto = false,
            };
            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response.Result;

            //TipoNotif Cita =1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_Cita = cita.Id_Cita,
                TipoNotif = 1,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
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

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.empresa = MainViewModel.GetInstance().UserHome.EmpresaList.Single(e => e.Id_Empresa == this.tecnico.Id_Empresa);
                    this.local = MainViewModel.GetInstance().UserHome.LocalesList.Single(l => l.Id_Local == this.tecnico.Id_Local);
                    this.LoadPostal();

                    this.thisPage = "Metodo";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentMetodoPopupPage());

                    break;

                case "Metodo":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    if (this.typeAppointment == true && this.PresupuestoPage != true)
                    {
                        this.thisPage = "Feature";
                        this.smallChecked = true;
                        this.easyChecked = true;
                        this.IsVisible = false;
                        this.IsVisible2 = true;
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentFeaturesPopupPage());

                        break;
                    }
                    else if(this.typeAppointment != true && this.PresupuestoPage != true)
                    {
                        this.IsVisible = true;
                        this.IsVisible2 = false;
                        this.thisPage = "Description";
                        this.subject = $"{Languages.Tattoo} {this.cliente.Nombre} {this.cliente.Apellido}, {Languages.Personalized}";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDescriptionPopupPage());
                        break;
                    }

                    else if (this.PresupuestoPage == true && this.typeAppointment != true)
                    {
                        this.thisPage = "Calendar";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
                        break;
                    }
                    break;
                case "Feature":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Calendar";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
                    break;

                case "Calendar":
                    if(this.CashMetodoChecked == true || (this.CashMetodoChecked == true && this.PresupuestoPage == true) || this.changeDate == true)
                    {
                        var tempDate = DateTime.Today.AddHours(48);
                        if (this.SelectedDate < tempDate)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                                Languages.Error,
                                Languages.Date48Hours,
                                "Ok");
                            break;
                        }
                    }
                    if (this.SelectedDate < DateTime.Today)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.DateBeforeTodayError,
                            "Ok");
                        break;
                    }
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    if (this.PresupuestoPage == true)
                    {
                        this.empresa = MainViewModel.GetInstance().UserHome.EmpresaList.Single(e => e.Id_Empresa == this.tecnico.Id_Empresa);
                        this.local = MainViewModel.GetInstance().UserHome.LocalesList.Single(l => l.Id_Local == this.tecnico.Id_Local);
                        this.LoadPostal();
                        this.HeightWidth = $"{Languages.MaximunSize}: {this.feature.Alto} cm X {this.feature.Ancho} cm";
                    }
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
                    if(this.PresupuestoPage == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.LoadInfoLocal();
                        this.thisPage = "Details";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDetailsPopupPage());
                        break;
                    }
                    if (this.changeDate == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.ChangeDate();
                        break;
                    }
                    if (this.addNewDate == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.AddNewDate();
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Description";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDescriptionPopupPage());
                    break;

                case "Description":
                    
                    if (string.IsNullOrEmpty(this.describeArt))
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.DescribeArtError,
                        "Ok");
                        break;
                    }
                    if (this.ArtImageSource == null || this.file == null)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.ArtError,
                        "Ok");
                        break;
                    }
                    if (this.typeAppointment == false)
                    {
                        if (string.IsNullOrEmpty(this.height) || string.IsNullOrEmpty(this.width))
                        {
                            await Application.Current.MainPage.DisplayAlert(
                            Languages.Error,
                            Languages.SizeError,
                            "Ok");
                            break;
                        }

                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.SavePersonal();
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();

                    this.LoadInfoLocal();
                    this.thisPage = "Details";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDetailsPopupPage());
                    break;

                case "Details":
                    Double thisAdvance = Double.Parse(tempAdvance.ToString());
                if(PayPalMetodoChecked == true)
                {
                    var result = await CrossPayPalManager.Current.Buy(new PayPalItem(Subject, comision, "MXN"), this.tempAdvance);
                    if (result.Status == PayPalStatus.Cancelled)
                    {
                        Debug.WriteLine("Cancelled");
                        break;
                    }
                    else if (result.Status == PayPalStatus.Error)
                    {
                        Debug.WriteLine(result.ErrorMessage);
                        break;
                    }
                    else if (result.Status == PayPalStatus.Successful)
                    {
                        if (this.PresupuestoPage == true)
                        {
                            await Application.Current.MainPage.Navigation.PopPopupAsync();
                            this.SavePersonalDate();
                            Debug.WriteLine(result.ServerResponse.Response.Id);
                            break;
                        }
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.SaveQuickDate();
                        Debug.WriteLine(result.ServerResponse.Response.Id);
                        break;
                    }
                }
                if (this.PresupuestoPage == true)
                {
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.SavePersonalDate();
                    break;
                }
                await Application.Current.MainPage.Navigation.PopPopupAsync();
                this.SaveQuickDate();
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
                    if(!fromTecnitoPage == false)
                    {
                        this.tecnico = null;
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.thisPage = null;
                        break;
                    }
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Search";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new SearchTecnicoPopupPage());
                    break;

                case "Metodo":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Type";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new TypeAppointmentPopupPage());
                    break;

                case "Feature":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Metodo";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentMetodoPopupPage());
                    break;

                case "Calendar":
                    if(this.PresupuestoPage==true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.thisPage = "Metodo";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentMetodoPopupPage());
                        MainViewModel.GetInstance().UserMessageJob.LoadBudget();
                        break;
                    }
                    if (this.changeDate == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        break;
                    }
                    if (this.addNewDate == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        break;
                    }
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Feature";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentFeaturesPopupPage());
                    break;

                case "Time":

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.newCita = null;
                    this.CitaList = null;
                    this.thisPage = "Calendar";
                    this.selectedTime = DateTime.Now;
                    this.SelectedDate = DateTime.Now;
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
                    break;

                case "Description":

                    if (this.typeAppointment == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.describeArt = null;
                        this.artImageSource = null;
                        this.file = null;
                        this.thisPage = "Time";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentTimePopupPage());
                        break;
                    }

                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.IsVisible = false;
                    this.describeArt = null;
                    this.artImageSource = null;
                    this.file = null;
                    this.thisPage = "Metodo";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentMetodoPopupPage());
                    break;

                case "Details":
                    if(this.PresupuestoPage == true)
                    {
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                        this.thisPage = "Time";
                        await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentTimePopupPage());
                        break;
                    }
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.thisPage = "Description";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentDescriptionPopupPage());
                    break;
            }
        }
        #endregion
    }
}
