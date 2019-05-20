﻿namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using Models;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Xamarin.Forms;

    public class UserViewDateViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public string cancelPage;
        private INavigation Navigation;

        private bool isRefreshing;
        private bool isVisible;
        private bool isButtonEnabled;

        private string address;
        private string studio;
        private string reference;
        private string subTotal;
        private string advance;
        private string total;
        public string hourStart;
        public string dateStart;

        private DateTime Date;
        
        private T_trabajocitas cita;
        public T_usuarios user;
        private ClientesCollection cliente;
        private T_trabajos trabajo;
        private TecnicosCollection tecnico;
        private T_locales local;
        private T_empresas empresa;
        private T_estado estado;
        private T_ciudad ciudad;
        private T_postal postal;
        public TrabajoNotaCollection notaSelected;
        public T_citaimagenes image;

        private ObservableCollection<NotasItemViewModel> notas;
        #endregion

        #region Properties

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
        public bool IsButtonEnabled
        {
            get { return this.isButtonEnabled; }
            set { SetValue(ref this.isButtonEnabled, value); }
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
        public string SubTotal
        {
            get { return this.subTotal; }
            set { SetValue(ref this.subTotal, value); }
        }
        public string Advance
        {
            get { return this.advance; }
            set { SetValue(ref this.advance, value); }
        }
        public string Total
        {
            get { return this.total; }
            set { SetValue(ref this.total, value); }
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

        public List<T_trabajonota> NotaList { get; set; }

        public T_trabajocitas Cita
        {
            get { return this.cita; }
            set { SetValue(ref this.cita, value); }
        }
        public T_citaimagenes Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }
        public T_trabajos Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public ObservableCollection<NotasItemViewModel> Notas
        {
            get { return this.notas; }
            set { SetValue(ref this.notas, value); }
        }

        #endregion

        #region Constructors
        public UserViewDateViewModel(T_trabajocitas cita, T_usuarios user, ClientesCollection cliente)
        {
            this.cita = cita;
            this.user = user;
            this.cliente = cliente;
            this.apiService = new ApiService();
            Task.Run(async () => { await this.LoadInfo(); }).Wait();
            Task.Run(async () => { await this.LoadNotas(); }).Wait();
            this.IsButtonEnabled = false;
            this.IsVisible = false;
            this.IsRefreshing = false;
            this.Date = new DateTime
            (
                this.cita.F_Inicio.Year,
                this.cita.F_Inicio.Month,
                this.cita.F_Inicio.Day,
                this.cita.H_Inicio.Hours,
                this.cita.H_Inicio.Minutes, 0
            );

            this.DateStart = this.Date.ToString("dd MMM yyyy");
            this.HourStart = this.Date.ToString("h:mm tt");
        }
        #endregion

        #region Commands
        public ICommand AddNewCommentCommand
        {
            get
            {
                return new RelayCommand(GoToAddCommandPopup);
            }
        }
        public ICommand EditCommentCommand
        {
            get
            {
                return new RelayCommand(GoToEditCommentPopup);
            }
        }


        public ICommand DeleteCommentCommand
        {
            get
            {
                return new RelayCommand(DeleteComent);
            }
        }

        public ICommand RefreshNotasCommand
        {
            get
            {
                return new RelayCommand(RefreshListNotas);
            }
        }
        public ICommand AcceptDateCommand
        {
            get
            {
                return new RelayCommand(ChangeDate);
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }
        public ICommand CancelDateCommand
        {
            get
            {
                return new RelayCommand(CancelDate);
            }
        }
        public ICommand CancelTempDateCommand
        {
            get
            {
                return new RelayCommand(CancelTempDate);
            }
        }

        #endregion

        #region Methods
        public async Task LoadInfo()
        {
            this.trabajo = MainViewModel.GetInstance().UserHome.TrabajosList.Where(t => t.Id_Trabajo == this.cita.Id_Trabajo).FirstOrDefault();

            this.subTotal = $"{Languages.Total}: {this.trabajo.Total_Aprox.ToString("C2")}";
            this.advance = $"{Languages.Advance} {this.trabajo.Costo_Cita.ToString("C2")}";
            var tot = this.trabajo.Total_Aprox - this.trabajo.Costo_Cita;
            this.total = $"{Languages.Remaining} {tot.ToString("C2")}";

            var tecnicoTemp = MainViewModel.GetInstance().UserHome.TecnicoList.Where(t => t.Id_Tecnico == this.cita.Id_Tatuador).FirstOrDefault();
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;

            this.tecnico = new TecnicosCollection
            {
                Id_Empresa = tecnicoTemp.Id_Empresa,
                Apellido = tecnicoTemp.Apellido,
                Apellido2 = tecnicoTemp.Apellido2,
                Apodo = tecnicoTemp.Apodo,
                Carrera = tecnicoTemp.Carrera,
                Id_Local = tecnicoTemp.Id_Local,
                Id_Tecnico = tecnicoTemp.Id_Tecnico,
                Id_Usuario = tecnicoTemp.Id_Usuario,
                Nombre = tecnicoTemp.Nombre,
                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == tecnicoTemp.Id_Usuario).F_Perfil,
            };

            this.local = MainViewModel.GetInstance().UserHome.LocalesList.Where(l => l.Id_Local == this.tecnico.Id_Local).FirstOrDefault();
            this.reference = $"{Languages.Reference} {this.local.Referencia}";

            this.empresa = MainViewModel.GetInstance().UserHome.EmpresaList.Where(e => e.Id_Empresa == this.tecnico.Id_Empresa).FirstOrDefault();
            this.studio = $"{this.empresa.Nombre} {Languages.BranchOffice} {this.local.Nombre}";

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

            //-----------------Cargar Datos Ciudad-----------------//

            controller = Application.Current.Resources["UrlT_ciudadController"].ToString();

            response = await this.apiService.Get<T_ciudad>(urlApi, prefix, controller, this.local.Id_Ciudad);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.ciudad = (T_ciudad)response.Result;

            //-----------------Cargar Datos Estado-----------------//

            controller = Application.Current.Resources["UrlT_estadoController"].ToString();

            response = await this.apiService.Get<T_estado>(urlApi, prefix, controller, this.local.Id_Estado);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.estado = (T_estado)response.Result;

            this.address = $"{this.local.Calle} {this.local.Numero}, {this.postal.Asentamiento} {this.postal.Colonia}, C.P. {this.postal.Id.ToString()}, {this.ciudad.Ciudad}, {this.estado.Estado}.";
            
            //-----------------Cargar Datos Imagenes-----------------//

            controller = Application.Current.Resources["UrlT_citaimagenesController"].ToString();

            response = await this.apiService.Get<T_citaimagenes>(urlApi, prefix, controller, this.trabajo.Id_Trabajo);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.image = (T_citaimagenes)response.Result;
        }
        public async Task LoadNotas()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {

                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_trabajonotaController"].ToString();

            var response = await this.apiService.GetList<T_trabajonota>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {

                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.NotaList = (List<T_trabajonota>)response.Result;

            this.RefreshListNotas();

            this.IsRefreshing = false;
        }
        public void RefreshListNotas()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            var nota = this.NotaList.Select(c => new NotasItemViewModel
            {
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,
                Id_Usuario = c.Id_Usuario,
                Tipo_Usuario = c.Tipo_Usuario,
                F_nota = c.F_nota,
                Id_Local = c.Id_Local,
                Id_Nota = c.Id_Nota,
                Nota = c.Nota,
                Nombre_Post = c.Nombre_Post,
                Cambio_Fecha = c.Cambio_Fecha,
                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == c.Id_Usuario).F_Perfil,                

            }).Where(c => c.Id_Cita == this.cita.Id_Cita).ToList();

            this.Notas = new ObservableCollection<NotasItemViewModel>(nota.OrderByDescending(c => c.F_nota));
        }
        public async void ChangeDate()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            var citaTemp = MainViewModel.GetInstance().UserHome.CitaList.Where(c => c.CitaTemp == true && c.Id_Trabajo == this.trabajo.Id_Trabajo).FirstOrDefault();
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
            var newCita = new T_trabajocitas
            {
                Id_Cita = this.cita.Id_Cita,
                Id_Trabajo = this.cita.Id_Trabajo,
                Id_Cliente = this.cita.Id_Cliente,
                Id_Tatuador = this.cita.Id_Tatuador,
                F_Inicio = this.cita.F_Inicio,
                H_Inicio = this.cita.H_Inicio,
                F_Fin = this.cita.F_Fin,
                H_Fin = this.cita.H_Fin,
                Asunto = this.cita.Asunto,
                Completa = this.cita.Completa,
                ColorText = this.cita.ColorText,
                CambioFecha = false,
                CitaTemp = false,
                TecnicoTiempo = true,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            var response = await this.apiService.Put(urlApi, prefix, controller, newCita,this.cita.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var citaAccepted = (T_trabajocitas)response.Result;

            newCita = new T_trabajocitas
            {
                Id_Cita = citaTemp.Id_Cita,
                Id_Trabajo = citaTemp.Id_Trabajo,
                Id_Cliente = citaTemp.Id_Cliente,
                Id_Tatuador = citaTemp.Id_Tatuador,
                F_Inicio = citaTemp.F_Inicio,
                H_Inicio = citaTemp.H_Inicio,
                F_Fin = citaTemp.F_Fin,
                H_Fin = citaTemp.H_Fin,
                Asunto = citaTemp.Asunto,
                Completa = citaTemp.Completa,
                ColorText = citaTemp.ColorText,
                CambioFecha = false,
                CitaTemp = false,
                TecnicoTiempo = false,
            };
            controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, newCita, citaTemp.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var citaTempAccepted = (T_trabajocitas)response.Result;

            var oldnota = this.NotaList.Where(t => t.Id_Cita == this.cita.Id_Cita && t.Cambio_Fecha == true).FirstOrDefault();
            controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            response = await this.apiService.Delete(urlApi, prefix, controller, oldnota.Id_Nota);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            if (oldnota != null)
            {
                this.NotaList.Remove(oldnota);
            }

            foreach (var notaTemp in this.NotaList)
            {
                var nota = new T_trabajonota
                {
                    Id_Nota = notaTemp.Id_Nota,
                    Id_Trabajo = notaTemp.Id_Trabajo,
                    Tipo_Usuario = notaTemp.Tipo_Usuario,
                    Id_Usuario = notaTemp.Id_Usuario,
                    Id_Local = notaTemp.Id_Local,
                    Id_Cita = citaTemp.Id_Cita,
                    Nota = notaTemp.Nota,
                    Nombre_Post = notaTemp.Nombre_Post,
                    F_nota = notaTemp.F_nota,
                    Cambio_Fecha = notaTemp.Cambio_Fecha,
                    
                };

                controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

                response = await this.apiService.Put(urlApi, prefix, controller, nota, notaTemp.Id_Nota);

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

            var oldCita = MainViewModel.GetInstance().UserHome.CitaList.Where(c => c.Id_Cita == this.cita.Id_Cita).FirstOrDefault();

            if (oldCita != null)
            {                
                MainViewModel.GetInstance().UserHome.CitaList.Remove(oldCita);
            }
            oldCita = MainViewModel.GetInstance().UserHome.CitaList.Where(c => c.Id_Cita == citaTemp.Id_Cita).FirstOrDefault();

            if (oldCita != null)
            {
                MainViewModel.GetInstance().UserHome.CitaList.Remove(oldCita);
            }

            MainViewModel.GetInstance().UserHome.CitaList.Add(citaAccepted);
            MainViewModel.GetInstance().UserHome.CitaList.Add(citaTempAccepted);
            MainViewModel.GetInstance().UserHome.RefreshCitaList();

            this.apiService.EndActivityPopup();

            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();

            if(this.cancelPage != "CancelDate")
            {
                if (this.cancelPage == "CancelTempDate")
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new CancelDatePopupPage());
                    this.cancelPage = null;
                    return;
                }
                if (string.IsNullOrEmpty(this.cancelPage))
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ChangeDatePopupPage());
                    this.cancelPage = "CancelTempDate";
                    return;
                }
            }

            this.cancelPage = null;
            
        }
        private async void CancelDate()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();

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
                Alto = this.trabajo.Alto,
                Ancho = this.trabajo.Ancho,
                Asunto = this.trabajo.Asunto,
                Cancelado = true,
                Completo = this.trabajo.Completo,
                Costo_Cita = this.trabajo.Costo_Cita,
                Id_Caract = this.trabajo.Id_Caract,
                Id_Cliente = this.trabajo.Id_Cliente,
                Id_Tatuador = this.trabajo.Id_Tatuador,
                Id_Trabajo = this.trabajo.Id_Trabajo,
                TecnicoTiempo = this.trabajo.TecnicoTiempo,
                Tiempo = this.trabajo.Tiempo,
                Total_Aprox = this.trabajo.Total_Aprox,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Put(urlApi, prefix, controller, newTrabajo, this.trabajo.Id_Trabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newAddedTrabajo = (T_trabajos)response.Result;

            var oldTrabajo = MainViewModel.GetInstance().UserHome.TrabajosList.Where(c => c.Id_Trabajo == this.cita.Id_Trabajo).FirstOrDefault();

            if (oldTrabajo != null)
            {
                MainViewModel.GetInstance().UserHome.TrabajosList.Remove(oldTrabajo);
            }

            MainViewModel.GetInstance().UserHome.TrabajosList.Add(newAddedTrabajo);

            MainViewModel.GetInstance().UserHome.RefreshCitaList();

            this.apiService.EndActivityPopup();

            await Application.Current.MainPage.Navigation.PopModalAsync();

        }
        private async void CancelTempDate()
        {
            if(cancelPage == "CancelDate")
            {
                this.CancelDate();
                return;
            }
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            var citaTemp = MainViewModel.GetInstance().UserHome.CitaList.Where(c => c.CitaTemp == true && c.Id_Trabajo == this.trabajo.Id_Trabajo).FirstOrDefault();

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
                Alto = this.trabajo.Alto,
                Ancho = this.trabajo.Ancho,
                Asunto = this.trabajo.Asunto,
                Cancelado = this.trabajo.Cancelado,
                Completo = this.trabajo.Completo,
                Costo_Cita = this.trabajo.Costo_Cita,
                Id_Caract = this.trabajo.Id_Caract,
                Id_Cliente = this.trabajo.Id_Cliente,
                Id_Tatuador = this.trabajo.Id_Tatuador,
                Id_Trabajo = this.trabajo.Id_Trabajo,
                TecnicoTiempo = true,                
                Tiempo = this.trabajo.Tiempo,
                Total_Aprox = this.trabajo.Total_Aprox,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Put(urlApi, prefix, controller, newTrabajo, this.trabajo.Id_Trabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newAddedTrabajo = (T_trabajos)response.Result;

            var oldTrabajo = MainViewModel.GetInstance().UserHome.TrabajosList.Where(c => c.Id_Trabajo == this.cita.Id_Trabajo).FirstOrDefault();

            if (oldTrabajo != null)
            {
                MainViewModel.GetInstance().UserHome.TrabajosList.Remove(oldTrabajo);
            }

            MainViewModel.GetInstance().UserHome.TrabajosList.Add(newAddedTrabajo);

            var newCita = new T_trabajocitas
            {
                Id_Cita = this.cita.Id_Cita,
                Id_Trabajo = this.cita.Id_Trabajo,
                Id_Cliente = this.cita.Id_Cliente,
                Id_Tatuador = this.cita.Id_Tatuador,
                F_Inicio = this.cita.F_Inicio,
                H_Inicio = this.cita.H_Inicio,
                F_Fin = this.cita.F_Fin,
                H_Fin = this.cita.H_Fin,
                Asunto = this.cita.Asunto,
                Completa = this.cita.Completa,
                ColorText = this.cita.ColorText,
                CambioFecha = false,
                CitaTemp = false,
                TecnicoTiempo = true,
            };
            controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.Put(urlApi, prefix, controller, newCita, this.cita.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var citaAccepted = (T_trabajocitas)response.Result;

            var oldCita = MainViewModel.GetInstance().UserHome.CitaList.Where(c => c.Id_Cita == this.cita.Id_Cita).FirstOrDefault();

            if (oldCita != null)
            {
                MainViewModel.GetInstance().UserHome.CitaList.Remove(oldCita);
            }
            
            MainViewModel.GetInstance().UserHome.CitaList.Add(citaAccepted);

            response = await this.apiService.Delete(urlApi, prefix, controller, citaTemp.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            oldCita = MainViewModel.GetInstance().UserHome.CitaList.Where(c => c.Id_Cita == citaTemp.Id_Cita).FirstOrDefault();

            if (oldCita != null)
            {
                MainViewModel.GetInstance().UserHome.CitaList.Remove(oldCita);
            }

            MainViewModel.GetInstance().UserHome.RefreshCitaList();

            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            var notaTempList = this.NotaList.Select(c => new NotasItemViewModel
            {
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,
                Id_Usuario = c.Id_Usuario,
                Tipo_Usuario = c.Tipo_Usuario,
                F_nota = c.F_nota,
                Id_Local = c.Id_Local,
                Id_Nota = c.Id_Nota,
                Nota = c.Nota,
                Nombre_Post = c.Nombre_Post,
                Cambio_Fecha = c.Cambio_Fecha,
                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == c.Id_Usuario).F_Perfil,

            }).Where(c => c.Id_Cita == this.cita.Id_Cita).ToList();

            foreach(var notaTemp in notaTempList)
            {
                controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

                response = await this.apiService.Delete(urlApi, prefix, controller, notaTemp.Id_Nota);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                var oldNotaTemp = this.NotaList.Where(n => n.Id_Trabajo == notaTemp.Id_Nota).FirstOrDefault();

                if (oldNotaTemp != null)
                {
                    this.NotaList.Remove(oldNotaTemp);
                }
            }

            this.apiService.EndActivityPopup();

            await Application.Current.MainPage.Navigation.PopModalAsync();

        }
        public void GoToAddCommandPopup()
        {
            MainViewModel.GetInstance().AddCommentPopup = new AddCommentPopupViewModel(user, cliente, tecnico, trabajo, cita);
            Navigation.PushPopupAsync(new AddCommentPopupPage());
        }
        private async void DeleteComent()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Notice,
                Languages.DeleteComment,
                Languages.Yes,
                Languages.No);
            if (!answer)
            {
                return;
            }

            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            var response = await this.apiService.Delete(urlApi, prefix, controller, this.notaSelected.Id_Nota);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var deletedNota = this.NotaList.Where(n => n.Id_Nota == this.notaSelected.Id_Nota).FirstOrDefault();
            if (deletedNota != null)
            {
                this.NotaList.Remove(deletedNota);
            }
            this.RefreshListNotas();
            this.notaSelected = null;
            IsButtonEnabled = false;
            IsVisible = false;
            this.IsRefreshing = false;
        }
        private void GoToEditCommentPopup()
        {
            MainViewModel.GetInstance().EditCommentPopup = new EditCommentPopupViewModel(notaSelected, user);
            Navigation.PushPopupAsync(new EditCommentPopupPage());
        }

        #endregion

    }
}
