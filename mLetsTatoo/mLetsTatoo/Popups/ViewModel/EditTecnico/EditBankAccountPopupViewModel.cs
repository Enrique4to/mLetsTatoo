namespace mLetsTatoo.Popups.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

    public class EditBankAccountPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private Bancos banco;
        private TecnicosCollection tecnico;
        private T_datos_bancarios datoBancario;
        private ObservableCollection<object> bancos;
        private string selectedBank;
        private string accountCard;
        private string bankAccountUserName;

        #endregion

        #region Properties
        public List<Bancos> BancosList { get; set; }
        public ObservableCollection<object> Bancos
        {
            get { return this.bancos; }
            set { SetValue(ref this.bancos, value); }
        }

        public string SelectedBank
        {
            get { return this.selectedBank; }
            set { SetValue(ref this.selectedBank, value); }
        }
        public string AccountCard
        {
            get { return this.accountCard; }
            set { SetValue(ref this.accountCard, value); }
        }
        public string BankAccountUserName
        {
            get { return this.bankAccountUserName; }
            set { SetValue(ref this.bankAccountUserName, value); }
        }
        #endregion

        #region Constructors
        public EditBankAccountPopupViewModel()
        {
            this.tecnico = MainViewModel.GetInstance().TecnicoHome.tecnico;
            this.apiService = new ApiService();
            this.LoadBanks();
            this.LoadInfo();
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

        private void LoadInfo()
        {
            if (MainViewModel.GetInstance().Login.DatosBancariosList.Any(b => b.Id_Usuario == this.tecnico.Id_Usuario))
            {
                this.datoBancario = MainViewModel.GetInstance().Login.DatosBancariosList.FirstOrDefault(b => b.Id_Usuario == tecnico.Id_Usuario);
                this.BankAccountUserName = this.datoBancario.Nombre;
                this.AccountCard = this.datoBancario.Cuenta;
                this.SelectedBank = this.datoBancario.Banco;
            }
        }
        public void LoadBanks()
        {
            BancosList = new List<Bancos>();
            banco = new Bancos { Nombre = "BANAMEX" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANCOMEXT" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANOBRAS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BBVA BANCOMER" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SANTANDER" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANJERCITO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "HSBC" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BAJIO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "IXE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "INBURSA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "INTERACCIONES" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "MIFEL" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SCOTIABANK" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANREGIO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "INVEX" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANSI" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "AFIRME" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "THE ROYAL BANK" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "AMERICAN EXPRESS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BAMSA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "TOKYO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "JP MORGAN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BMONEX" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "VE POR MAS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ING" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "DEUTSCHE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CREDIT SUISSE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "AZTECA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "AUTOFIN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BARCLAYS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "COMPARTAMOS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANCO FAMSA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BMULTIVA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ACTINVER" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "WAL-MART" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "NAFIN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "INTERBANCO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANCOPPEL" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ABC CAPITAL" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "UBS BANK" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CONSUBANCO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "VOLKSWAGEN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CIBANCO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BBASE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BANSEFI" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "HIPOTECARIA FEDERAL" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "MONEXCB" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "GBM" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "MASARI" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "VALUE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ESTRUCTURADORES" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "TIBER" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "VECTOR" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "B&B" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ACCIVAL" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "MERRILL LYNCH" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "FINAMEX" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "VALMEX" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "UNICA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "MAPFRE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "PROFUTURO" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CB ACTINVER" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "OACTIN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SKANDIA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CBDEUTSCHE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ZURICH" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ZURICHVI" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SU CASITA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CB INTERCAM" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CI BOLSA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "BULLTICK CB" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "STERLING" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "FINCOMUN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "HDI SEGUROS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ORDER" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "AKALA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CB JPMORGAN" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "REFORMA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "STP" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "TELECOMM" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "EVERCORE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SKANDIA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SEGMTY" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "ASEA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "KUSPIT" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "SOFIEXPRESS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "UNAGRA" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "OPCIONES EMPRESARIALES DEL NOROESTE" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "CLS" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "INDEVAL" };
            BancosList.Add(banco);
            banco = new Bancos { Nombre = "LIBERTAD" };
            BancosList.Add(banco);
            this.BancosList.OrderBy(b => b.Nombre);
            this.Bancos = new ObservableCollection<object>();
            foreach(var bank in BancosList)
            {
                this.Bancos.Add(bank.Nombre);
            }
            this.Bancos = new ObservableCollection<object>(this.Bancos.OrderBy(i => i));
        }
        private async void GoToNextPopupPage()
        {
            if (MainViewModel.GetInstance().Login.DatosBancariosList.Any(b => b.Id_Usuario == this.tecnico.Id_Usuario))
            {
                var newDatoBancario = new T_datos_bancarios
                {
                    Id_DatoBancario = this.datoBancario.Id_DatoBancario,
                    Id_Usuario = this.tecnico.Id_Usuario,
                    Nombre = this.BankAccountUserName,
                    Cuenta = this.AccountCard,
                    Banco = this.SelectedBank,
                };

                var urlApi = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlT_datos_bancariosController"].ToString();

                var response = await this.apiService.Put(urlApi, prefix, controller, newDatoBancario, this.datoBancario.Id_DatoBancario);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                newDatoBancario = (T_datos_bancarios)response.Result;
                var oldDatoBancario = MainViewModel.GetInstance().Login.DatosBancariosList.FirstOrDefault(b => b.Id_Usuario == tecnico.Id_Usuario);
                if(oldDatoBancario != null)
                {
                    MainViewModel.GetInstance().Login.DatosBancariosList.Remove(oldDatoBancario);
                }
                MainViewModel.GetInstance().Login.DatosBancariosList.Add(newDatoBancario);

                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }
            else
            {
                var newDatoBancario = new T_datos_bancarios
                {
                    Id_Usuario = this.tecnico.Id_Usuario,
                    Nombre = this.BankAccountUserName,
                    Cuenta = this.AccountCard,
                    Banco = this.SelectedBank,
                };

                var urlApi = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlT_datos_bancariosController"].ToString();

                var response = await this.apiService.Post(urlApi, prefix, controller, newDatoBancario);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                newDatoBancario = (T_datos_bancarios)response.Result;
                MainViewModel.GetInstance().Login.DatosBancariosList.Add(newDatoBancario);

                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }
        }
        private async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        #endregion

    }
}
