namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using Models;
    using Views;
    using Xamarin.Forms;
    using System;

    public class NotificacionesItemViewModel : T_notificaciones
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        public T_trabajos trabajo;
        public CitasItemViewModel cita;
        public TecnicosCollection tecnico;
        public ClientesCollection cliente;
        public T_usuarios user;
        public TrabajosTempItemViewModel trabajoTemp;
        #endregion
        #region Properties
        public int Id_Cita { get; set; }
        public int Id_TrabajoTemp { get; set; }
        public int TipoNotif { get; set; }
        public List<T_trabajonotatemp> TrabajoNotaList { get; set; }
        #endregion

        #region Contructors
        public NotificacionesItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand NotificationCommand
        {
            get
            {
                return new RelayCommand(OpenNotificacion);
            }
        }
        #endregion

        #region Nethods

        private async void OpenNotificacion()
        {
            if (MainViewModel.GetInstance().Login.user.Tipo == 1)
            {
                this.user = MainViewModel.GetInstance().UserHome.user;
                this.cliente = MainViewModel.GetInstance().UserHome.cliente;
                this.tecnico = MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Usuario == this.Usuario_Envia);

                if (this.TipoNotif == 1)
                {
                    if(MainViewModel.GetInstance().UserHome.CteCitaList.Any(c => c.Id_Cita == this.Id_Cita 
                    && (c.Completa == false || c.Cancelado == false || c.Completado == false || c.TecnicoTiempo == false)))
                    {
                        this.cita = MainViewModel.GetInstance().UserHome.CteCitaList.FirstOrDefault(c => c.Id_Cita == this.Id_Cita);
                        this.trabajo = MainViewModel.GetInstance().Login.TrabajosList.FirstOrDefault(c => c.Id_Trabajo == this.cita.Id_Trabajo);

                        MainViewModel.GetInstance().UserViewDate = new UserViewDateViewModel(this.cita, this.user, this.cliente);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new UserViewDatePage());
                    }
                    return;

                }
                else if (this.TipoNotif == 2)
                {
                        MainViewModel.GetInstance().UserMessages = new UserMessagesViewModel(this.user, this.cliente);
                        this.trabajoTemp = MainViewModel.GetInstance().UserMessages.TrabajoTempList.FirstOrDefault(c => c.Id_Trabajotemp == this.Id_TrabajoTemp);

                        MainViewModel.GetInstance().UserMessageJob = new UserMessageJobViewModel(this.trabajoTemp, this.cliente, this.user);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new UserMessajeJobPage());
                }
            }
            else
            {
                this.tecnico = MainViewModel.GetInstance().TecnicoHome.tecnico;
                this.user = MainViewModel.GetInstance().TecnicoHome.user;
                this.cliente = MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault(c => c.Id_Usuario == this.Usuario_Envia);

                if (this.TipoNotif == 1)
                {
                    var citatemp = MainViewModel.GetInstance().Login.CitaList.FirstOrDefault(c => c.Id_Cita == this.Id_Cita);
                    this.trabajo = MainViewModel.GetInstance().TecnicoHome.TrabajoList.FirstOrDefault(c => c.Id_Trabajo == citatemp.Id_Trabajo);
                    MainViewModel.GetInstance().TecnicoViewJob = new TecnicoViewJobViewModel(this.trabajo, this.user, this.tecnico);
                    this.cita = MainViewModel.GetInstance().TecnicoViewJob.TcoCitasList.FirstOrDefault(c => c.Id_Cita == this.Id_Cita);
                    if (MainViewModel.GetInstance().TecnicoViewJob.TcoCitasList.Any(c => c.Id_Cita == this.Id_Cita
                        && (c.Completa == false || c.Cancelado == false || c.Completado == false || c.TecnicoTiempo == false)))
                    {
                        if (this.cita.Completa != true)
                        {
                            if (this.cita.Pagado == false)
                            {
                                var answer = await Application.Current.MainPage.DisplayAlert(Languages.Notice, Languages.TypeCashDate, Languages.Yes, Languages.No);
                                if (!answer)
                                {
                                    return;
                                }

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
                                var pago = MainViewModel.GetInstance().Login.ListPagosCliente.FirstOrDefault(p => p.Id_Trabajo == this.cita.Id_Trabajo);
                                var newPago = new T_pagoscliente
                                {
                                    Id_Pagocliente = pago.Id_Pagocliente,
                                    Id_Cliente = pago.Id_Cliente,
                                    Id_Trabajo = pago.Id_Trabajo,
                                    Id_Usuario = pago.Id_Usuario,
                                    Pago = pago.Pago,
                                    Pagado = true,
                                    Fecha_Pago = DateTime.Now,
                                    Fecha_Peticion = pago.Fecha_Peticion,
                                    Tipo_Pago = pago.Tipo_Pago,
                                };

                                var urlApi = Application.Current.Resources["UrlAPI"].ToString();
                                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                                var controller = Application.Current.Resources["UrlT_pagosclienteController"].ToString();

                                var response = await this.apiService.Put(urlApi, prefix, controller, newPago, pago.Id_Pagocliente);

                                if (!response.IsSuccess)
                                {
                                    this.apiService.EndActivityPopup();

                                    await Application.Current.MainPage.DisplayAlert(
                                    Languages.Error,
                                    response.Message,
                                    "OK");
                                    return;
                                }

                                newPago = (T_pagoscliente)response.Result;
                                var oldPago = MainViewModel.GetInstance().Login.ListPagosCliente.Where(p => p.Id_Pagocliente == pago.Id_Pagocliente).FirstOrDefault();
                                if (oldPago != null)
                                {
                                    MainViewModel.GetInstance().Login.ListPagosCliente.Remove(oldPago);
                                }
                                MainViewModel.GetInstance().Login.ListPagosCliente.Add(newPago);


                                if (!MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Tecnico == this.cita.Id_Tatuador))
                                {
                                    var newsaldo = new T_balancetecnico
                                    {
                                        Id_Tecnico = this.cita.Id_Tatuador,
                                        Id_Usuario = MainViewModel.GetInstance().TecnicoHome.User.Id_usuario,
                                        Saldo_Contra = 50,
                                        Saldo_Favor = 0,
                                        Saldo_Retenido = 0,
                                    };
                                    controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                                    response = await this.apiService.Post(urlApi, prefix, controller, newsaldo);

                                    if (!response.IsSuccess)
                                    {
                                        this.apiService.EndActivityPopup();

                                        await Application.Current.MainPage.DisplayAlert(
                                        Languages.Error,
                                        response.Message,
                                        "OK");
                                        return;
                                    }

                                    newsaldo = (T_balancetecnico)response.Result;
                                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newsaldo);
                                }
                                else
                                {
                                    var saldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Tecnico == this.cita.Id_Tatuador);
                                    var saldo_Favor = saldo.Saldo_Favor;
                                    var saldo_Contra = saldo.Saldo_Contra + 50;

                                    decimal i = 0;
                                    if (saldo_Contra > 0)
                                    {
                                        if (saldo_Favor >= saldo_Contra)
                                        {
                                            saldo_Favor = saldo_Favor - saldo_Contra;
                                            i = saldo_Contra;
                                            saldo_Contra = 0;
                                        }
                                        else
                                        {
                                            saldo_Contra = saldo_Contra - saldo_Favor;
                                            i = saldo_Favor;
                                            saldo_Favor = 0;
                                        }
                                    }
                                    if (i > 0)
                                    {
                                        var newPagoTecnico = new T_pagostecnico
                                        {
                                            Id_Tecnico = MainViewModel.GetInstance().TecnicoHome.tecnico.Id_Tecnico,
                                            Id_Usuario = MainViewModel.GetInstance().TecnicoHome.tecnico.Id_Usuario,
                                            Pago = i,
                                            Fecha_Pago = DateTime.Now,
                                            Concepto = Languages.PagoTecnicoConcept2,
                                        };
                                        controller = Application.Current.Resources["UrlT_pagostecnicoController"].ToString();

                                        response = await this.apiService.Post(urlApi, prefix, controller, newPagoTecnico);

                                        if (!response.IsSuccess)
                                        {
                                            this.apiService.EndActivityPopup();

                                            await Application.Current.MainPage.DisplayAlert(
                                            Languages.Error,
                                            response.Message,
                                            "OK");
                                            return;
                                        }

                                        newPagoTecnico = (T_pagostecnico)response.Result;
                                        MainViewModel.GetInstance().Login.ListPagosTecnico.Add(newPagoTecnico);
                                    }

                                    var newsaldo = new T_balancetecnico
                                    {
                                        Id_Balancetecnico = saldo.Id_Balancetecnico,
                                        Id_Tecnico = saldo.Id_Tecnico,
                                        Id_Usuario = saldo.Id_Usuario,
                                        Saldo_Contra = saldo_Contra,
                                        Saldo_Favor = saldo_Favor,
                                        Saldo_Retenido = saldo.Saldo_Retenido,
                                    };
                                    controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                                    response = await this.apiService.Put(urlApi, prefix, controller, newsaldo, saldo.Id_Balancetecnico);

                                    if (!response.IsSuccess)
                                    {
                                        this.apiService.EndActivityPopup();

                                        await Application.Current.MainPage.DisplayAlert(
                                        Languages.Error,
                                        response.Message,
                                        "OK");
                                        return;
                                    }

                                    newsaldo = (T_balancetecnico)response.Result;
                                    var oldSaldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.Where(p => p.Id_Balancetecnico == saldo.Id_Balancetecnico).FirstOrDefault();
                                    if (oldSaldo != null)
                                    {
                                        MainViewModel.GetInstance().Login.ListBalanceTecnico.Remove(oldSaldo);
                                    }
                                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newsaldo);
                                }

                                MainViewModel.GetInstance().TecnicoHome.LoadTecnico();
                                MainViewModel.GetInstance().TecnicoViewJob.RerfeshCitasList();
                            }

                            MainViewModel.GetInstance().TecnicoViewDate = new TecnicoViewDateViewModel(this.cita, this.user, this.tecnico, this.trabajo);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoViewDatePage());
                        }
                    }
                    return;
                }
                else if (this.TipoNotif == 2)
                {
                    MainViewModel.GetInstance().TecnicoMessages = new TecnicoMessagesViewModel(this.user, this.tecnico);
                    this.trabajoTemp = MainViewModel.GetInstance().TecnicoMessages.TrabajoTempList.FirstOrDefault(c => c.Id_Trabajotemp == this.Id_TrabajoTemp);

                    MainViewModel.GetInstance().TecnicoMessageJob = new TecnicoMessageJobViewModel(this.trabajoTemp, this.tecnico, this.user);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoMessajeJobPage());
                }
            }


        }
        #endregion
    }
}