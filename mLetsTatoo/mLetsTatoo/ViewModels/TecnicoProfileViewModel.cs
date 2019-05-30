namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Popups.ViewModel;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class TecnicoProfileViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private T_datos_bancarios datosBancarios;
        private string nombreCompleto;
        private string saldo_Favor;
        private string saldo_Retirar;
        private string saldo_Contra;
        private string saldo_Retenido;
        private string mensageRetiro;

        private byte[] byteImage;
        private ImageSource imageSource;
        private MediaFile file;

        private bool isRefreshing;
        private bool isRunning;

        public List<T_clientes> listClientes;

        public TecnicosCollection tecnico;
        public T_usuarios user;
        #endregion

        #region Properties
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public byte[] ByteImage
        {
            get { return this.byteImage; }
            set { SetValue(ref this.byteImage, value); }
        }
        public string NombreCompleto
        {
            get { return this.nombreCompleto; }
            set { SetValue(ref this.nombreCompleto, value); }
        }
        public string Saldo_Favor
        {
            get { return this.saldo_Favor; }
            set { SetValue(ref this.saldo_Favor, value); }
        }
        public string Saldo_Retirar
        {
            get { return this.saldo_Retirar; }
            set { SetValue(ref this.saldo_Retirar, value); }
        }
        public string Saldo_Contra
        {
            get { return this.saldo_Contra; }
            set { SetValue(ref this.saldo_Contra, value); }
        }
        public string Saldo_Retenido
        {
            get { return this.saldo_Retenido; }
            set { SetValue(ref this.saldo_Retenido, value); }
        }
        public string MensageRetiro
        {
            get { return this.mensageRetiro; }
            set { SetValue(ref this.mensageRetiro, value); }
        }
        #endregion

        #region Constructors
        public TecnicoProfileViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.NombreCompleto = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
            this.LoadUser();
        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        public ICommand OptionsCommand
        {
            get
            {
                return new RelayCommand(GoToOptions);
            }
        }
        public ICommand EditUserCommand
        {
            get
            {
                return new RelayCommand(GoToEditUser);
            }
        }
        public ICommand EditFeaturesCommand
        {
            get
            {
                return new RelayCommand(GoToEditFeatures);
            }
        }
        public ICommand EditSchedulerCommand
        {
            get
            {
                return new RelayCommand(GoToEditScheduler);
            }
        }
        public ICommand EditBankAccountCommand
        {
            get
            {
                return new RelayCommand(GoToEditBankAccount);
            }
        }

        public ICommand SignOutCommand
        {
            get
            {
                return new RelayCommand(SignOut);
            }
        }
        public ICommand ClosePopupCommand
        {
            get
            {
                return new RelayCommand(ClosePopup);
            }
        }
        public ICommand GetMoneyCommand
        {
            get
            {
                return new RelayCommand(GetMoneyPopup);
            }
        }
        public ICommand MoneyCommand
        {
            get
            {
                return new RelayCommand(MoneyPopup);
            }
        }


        #endregion
        #region Methods
        public void LoadUser()
        {
            if (this.user.F_Perfil != null)
            {
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.user.F_Perfil));
            }
            else
            {
                this.ByteImage = this.apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
            this.saldo_Favor = this.tecnico.Saldo_Favor.ToString("C2");
            this.saldo_Contra = this.tecnico.Saldo_Contra.ToString("C2");
            this.saldo_Retenido = this.tecnico.Saldo_Retenido.ToString("C2");
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
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    this.SavePic();
                    return stream;
                });
            }
        }
        private async void SavePic()
        {
            this.IsRunning = true;
            
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            if (this.file != null)
            {
                this.ByteImage = FileHelper.ReadFully(this.file.GetStream());
            }

            var newUser = new T_usuarios
            {
                Id_usuario = this.user.Id_usuario,
                Bloqueo = this.user.Bloqueo,
                Confirmacion = this.user.Confirmacion,
                Confirmado = this.user.Confirmado,
                F_Perfil = this.ByteImage,
                Pass = this.user.Pass,
                Tipo = this.user.Tipo,
                Ucorreo = this.user.Ucorreo,
                Usuario = this.user.Usuario,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_usuariosController"].ToString();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                newUser,
                this.user.Id_usuario);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var NewUser = (T_usuarios)response.Result;

            var oldUser = MainViewModel.GetInstance().Login.ListUsuarios.Where(n => n.Id_usuario == this.user.Id_usuario).FirstOrDefault();
            if (oldUser != null)
            {
                MainViewModel.GetInstance().Login.ListUsuarios.Remove(oldUser);
            }

            MainViewModel.GetInstance().Login.ListUsuarios.Add(NewUser);
            this.IsRunning = false;
        }
        private async void MoneyPopup()
        {
            if (MainViewModel.GetInstance().Login.DatosBancariosList.Any(b => b.Id_Usuario == this.tecnico.Id_Usuario))
            {
                this.datosBancarios = MainViewModel.GetInstance().Login.DatosBancariosList.FirstOrDefault(b => b.Id_Usuario == this.tecnico.Id_Usuario);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorNoBankAccount,
                    "Ok");
                return;
            }
            var saldoTemp = decimal.Parse(Saldo_Retirar);
            if (saldoTemp > this.tecnico.Saldo_Favor)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorGetMoney,
                    "Ok");
                return;
            }
            try
            {
                var newretiro = new T_retiros
                {
                    Id_Usuario = this.tecnico.Id_Usuario,
                    Retiro = saldoTemp,
                    Fecha_Retiro = DateTime.Today.ToLocalTime(),
                };

                var urlApi = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlT_retirosController"].ToString();

                var response = await this.apiService.Post(urlApi, prefix, controller, newretiro);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                var saldo = MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Tecnico == this.tecnico.Id_Tecnico);
                var newsaldo_Favor = saldo.Saldo_Favor - saldoTemp;

                var newsaldo = new T_balancetecnico
                {
                    Id_Balancetecnico = saldo.Id_Balancetecnico,
                    Id_Tecnico = saldo.Id_Tecnico,
                    Id_Usuario = saldo.Id_Usuario,
                    Saldo_Contra = saldo.Saldo_Contra,
                    Saldo_Favor = newsaldo_Favor,
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

                MainViewModel.GetInstance().TecnicoHome.LoadTecnico();
                this.Saldo_Favor = newsaldo_Favor.ToString("C2");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.korreoweb.com");

                mail.From = new MailAddress("informacion@letstattoo.com.mx");
                mail.To.Add("letstattoopagos@outlook.com");
                mail.Subject = $"Retiro de cuenta del Usuario: {this.tecnico.Nombre} {this.tecnico.Apellido}";
                mail.Body = $"El Usuario {this.tecnico.Nombre} {this.tecnico.Apellido} ha solicitado un retiro de su saldo a favor por un total de " +
                    $"{saldoTemp.ToString("C2")}. {'\n'}{'\n'} Los datos bancarios para la transferencia son: {'\n'} Nombre Completo:" +
                    $"{this.datosBancarios.Nombre}{'\n'} Correo Electrónico: {this.user.Ucorreo}{'\n'} Cuenta a depositar: {this.datosBancarios.Cuenta}" +
                    $"{'\n'} Banco: {this.datosBancarios.Banco}{'\n'}{'\n'} Total a depositar: {saldoTemp.ToString("C2")}{'\n'} {'\n'} {'\n'} " +
                    $"La información obtenida es solo para realizar depósitos de cuentas LetsTattoo a terceros.";

                SmtpServer.Port = 587;
                SmtpServer.Host = "mail.korreoweb.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("informacion@letstattoo.com.mx", "rXEG6dPkP3RRU5m8");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            await Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        private async void GoToOptions()
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync(new EditTecnicoMenuPopupPage());
        }

        private async void GoToEditFeatures()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            MainViewModel.GetInstance().EditFeaturesPopup = new EditFeaturesPopupViewModel();
            MainViewModel.GetInstance().EditFeaturesPopup.page = "Small";
            await Application.Current.MainPage.Navigation.PushPopupAsync(new EditSmallPopupPage());
        }
        private async void GoToEditUser()
        {
            MainViewModel.GetInstance().EditTecnicoUser = new EditTecnicoUserViewModel(this.tecnico, this.user);

            await Application.Current.MainPage.Navigation.PushModalAsync(new EditTecnicoUserPage());
        }
        private async void GoToEditScheduler()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            var horario = MainViewModel.GetInstance().TecnicoHome.horario;
            var localHorario = MainViewModel.GetInstance().Login.ListHorariosLocales.FirstOrDefault(l => l.Id_Local == this.tecnico.Id_Local);
            MainViewModel.GetInstance().EditSchedulerPopup = new EditSchedulerPopupViewModel(horario, localHorario);
            MainViewModel.GetInstance().EditSchedulerPopup.page = "Horario";
            MainViewModel.GetInstance().EditSchedulerPopup.Horario = MainViewModel.GetInstance().TecnicoHome.horario;
            await Application.Current.MainPage.Navigation.PushPopupAsync(new EditHorarioPopupPage());
        }
        private async void GoToEditBankAccount()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            MainViewModel.GetInstance().EditBankAccountPopup = new EditBankAccountPopupViewModel();
            await Application.Current.MainPage.Navigation.PushPopupAsync(new EditBankAccountPopupPage());
        }
        private void GetMoneyPopup()
        {
            this.MensageRetiro = $"{Languages.MaximunGetMoney} {this.Saldo_Favor}";
            this.Saldo_Retirar = this.tecnico.Saldo_Favor.ToString();
            Application.Current.MainPage.Navigation.PushPopupAsync(new GetMoneyPopupPage());
        }
        private void SignOut()
        {
        }

        private async void ClosePopup()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        #endregion
    }
}
