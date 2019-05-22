namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class CitasItemViewModel : T_trabajocitas
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        #endregion

        #region Properties
        public Color Color { get; set; }
        public bool Completado { get; set; }
        public bool Cancelado { get; set; }
        public bool Pagado { get; set; }
        public bool Trabajo_Iniciado { get; set; }
        #endregion

        #region Constructors
        public CitasItemViewModel()
        {
            this.apiService = new ApiService();
        }

        #endregion

        #region Commands
        public ICommand UserViewDatePageCommand
        {
            get
            {
                return new RelayCommand(GoToUserViewDatePage);
            }
        }
        public ICommand TecnicoViewDatePageCommand
        {
            get
            {
                return new RelayCommand(GoToTecnicoViewDatePage);
            }
        }
        #endregion

        #region Methods
        private void GoToUserViewDatePage()
        {
            var user = MainViewModel.GetInstance().Login.user;
            var cliente = MainViewModel.GetInstance().Login.cliente;
            MainViewModel.GetInstance().UserViewDate = new UserViewDateViewModel(this, user, cliente);
            Application.Current.MainPage.Navigation.PushModalAsync(new UserViewDatePage());
        }
        private async void GoToTecnicoViewDatePage()
        {
            if(this.Completa != true)
            {
                if (this.Pagado == false)
                {
                    var answer = await Application.Current.MainPage.DisplayAlert(Languages.Notice, Languages.TypeCashDate, Languages.Yes, Languages.No);
                    if(!answer)
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
                    var pago = MainViewModel.GetInstance().Login.ListPagosCliente.FirstOrDefault(p => p.Id_Trabajo == this.Id_Trabajo);
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


                    if (!MainViewModel.GetInstance().Login.ListBalanceTecnico.Any(b => b.Id_Tecnico == this.Id_Tatuador))
                    {
                        var newsaldo = new T_balancetecnico
                        {
                            Id_Tecnico = this.Id_Tatuador,
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
                        var saldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Tecnico == this.Id_Tatuador);
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

                var user = MainViewModel.GetInstance().Login.user;
                var tecnico = MainViewModel.GetInstance().Login.tecnico;
                var trabajo = MainViewModel.GetInstance().TecnicoViewJob.trabajo;
                MainViewModel.GetInstance().TecnicoViewDate = new TecnicoViewDateViewModel(this, user, tecnico, trabajo);
                await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoViewDatePage());
            }
        }
        //private async void TecnicoSelected()
        //{
        //    MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
        //    await Application.Current.MainPage.Navigation.PopModalAsync();
        //}
        #endregion
    }
}
