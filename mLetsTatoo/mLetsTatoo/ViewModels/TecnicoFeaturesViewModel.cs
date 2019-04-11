

namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Helpers;
    using Plugin.Media.Abstractions;
    using Services;
    using Xamarin.Forms;
    using mLetsTatoo.CustomPages;
    using mLetsTatoo.Views;

    public class TecnicoFeaturesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private decimal cost;
        private decimal advance;
        private decimal height;
        private decimal width;

        private string cost1;
        private string cost2;
        private string cost3;
        private string cost4;
        private string cost5;
        private string cost6;
        private string cost7;
        private string cost8;
        private string cost9;

        private string advance1;
        private string advance2;
        private string advance3;
        private string advance4;
        private string advance5;
        private string advance6;
        private string advance7;
        private string advance8;
        private string advance9;

        private string height1;
        private string height2;
        private string height3;

        private string width1;
        private string width2;
        private string width3;

        private T_usuarios user;
        private T_tecnicos tecnico;
        private T_teccaract feature;

        private bool isEnabled;
        private bool isRunning;

        private byte[] byteImage;

        public MediaFile file1;
        public MediaFile file2;
        public MediaFile file3;
        public MediaFile file4;
        public MediaFile file5;
        public MediaFile file6;
        public MediaFile file7;
        public MediaFile file8;
        public MediaFile file9;

        #endregion

        #region Properties
        public string Cost1
        {
            get { return this.cost1; }
            set { SetValue(ref this.cost1, value); }
        }
        public string Cost2
        {
            get { return this.cost2; }
            set { SetValue(ref this.cost2, value); }
        }
        public string Cost3
        {
            get { return this.cost3; }
            set { SetValue(ref this.cost3, value); }
        }
        public string Cost4
        {
            get { return this.cost4; }
            set { SetValue(ref this.cost4, value); }
        }
        public string Cost5
        {
            get { return this.cost5; }
            set { SetValue(ref this.cost5, value); }
        }
        public string Cost6
        {
            get { return this.cost6; }
            set { SetValue(ref this.cost6, value); }
        }
        public string Cost7
        {
            get { return this.cost7; }
            set { SetValue(ref this.cost7, value); }
        }
        public string Cost8
        {
            get { return this.cost8; }
            set { SetValue(ref this.cost8, value); }
        }
        public string Cost9
        {
            get { return this.cost9; }
            set { SetValue(ref this.cost9, value); }
        }

        public string Advance1
        {
            get { return this.advance1; }
            set { SetValue(ref this.advance1, value); }
        }
        public string Advance2
        {
            get { return this.advance2; }
            set { SetValue(ref this.advance2, value); }
        }
        public string Advance3
        {
            get { return this.advance3; }
            set { SetValue(ref this.advance3, value); }
        }
        public string Advance4
        {
            get { return this.advance4; }
            set { SetValue(ref this.advance4, value); }
        }
        public string Advance5
        {
            get { return this.advance5; }
            set { SetValue(ref this.advance5, value); }
        }
        public string Advance6
        {
            get { return this.advance6; }
            set { SetValue(ref this.advance6, value); }
        }
        public string Advance7
        {
            get { return this.advance7; }
            set { SetValue(ref this.advance7, value); }
        }
        public string Advance8
        {
            get { return this.advance8; }
            set { SetValue(ref this.advance8, value); }
        }
        public string Advance9
        {
            get { return this.advance9; }
            set { SetValue(ref this.advance9, value); }
        }

        public string Height1
        {
            get { return this.height1; }
            set { SetValue(ref this.height1, value); }
        }
        public string Height2
        {
            get { return this.height2; }
            set { SetValue(ref this.height2, value); }
        }
        public string Height3
        {
            get { return this.height3; }
            set { SetValue(ref this.height3, value); }
        }

        public string Width1
        {
            get { return this.width1; }
            set { SetValue(ref this.width1, value); }
        }
        public string Width2
        {
            get { return this.width2; }
            set { SetValue(ref this.width2, value); }
        }
        public string Width3
        {
            get { return this.width3; }
            set { SetValue(ref this.width3, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool IsRunning
        { 
                get { return this.isRunning; }
                set { SetValue(ref this.isRunning, value); }
        }

        //public byte[] ByteImage
        //{
        //    get { return this.byteImage; }
        //    set { SetValue(ref this.byteImage, value); }
        //}

        #endregion

        #region Constructors
        public TecnicoFeaturesViewModel(T_usuarios user)
        {

            Application.Current.MainPage.DisplayAlert(
                Languages.Notice,
                Languages.CompleteFeatures,
                "Ok");

            this.user = user;
            this.apiService = new ApiService();
            this.LoadFeatures();
            this.LoadTecnico();
            IsRunning = false;
            IsEnabled = true;
        }

        private void LoadFeatures()
        {
            //if (this.cliente.F_Perfil != null)
            //{
            //    this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.cliente.F_Perfil));
            //}
            //else
            //{
            //    this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
            //    this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            //}
        }
        #endregion

        #region Commands
        public ICommand NextCommand
        {
            get
            {
                return new RelayCommand(SaveAndNext);
            }
        }
        #endregion

        #region Merhods
        private async void LoadTecnico()
        {
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_tecnicosController"].ToString();

            var response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            var listcte = (List<T_tecnicos>)response.Result;

            this.tecnico = listcte.Single(t => t.Id_Usuario == this.user.Id_usuario);

            this.IsRunning = false;
        }

        private async void SaveAndNext()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            if (string.IsNullOrEmpty(this.cost1) 
                || string.IsNullOrEmpty(this.cost2)
                || string.IsNullOrEmpty(this.cost3)
                || string.IsNullOrEmpty(this.cost4)
                || string.IsNullOrEmpty(this.cost5)
                || string.IsNullOrEmpty(this.cost6)
                || string.IsNullOrEmpty(this.cost7)
                || string.IsNullOrEmpty(this.cost8)
                || string.IsNullOrEmpty(this.cost9))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CostError,
                    "Ok");
                return;
            }

            if (string.IsNullOrEmpty(this.advance1) 
                || string.IsNullOrEmpty(this.advance2)
                || string.IsNullOrEmpty(this.advance3)
                || string.IsNullOrEmpty(this.advance4)
                || string.IsNullOrEmpty(this.advance5)
                || string.IsNullOrEmpty(this.advance6)
                || string.IsNullOrEmpty(this.advance7)
                || string.IsNullOrEmpty(this.advance8)
                || string.IsNullOrEmpty(this.advance9))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AdvanceError,
                    "Ok");
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            #region Small
            if (string.IsNullOrEmpty(this.height1) || string.IsNullOrEmpty(this.Width1))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                return;
            }

            this.height = decimal.Parse(this.height1);
            this.width = decimal.Parse(this.width1);
            if (this.height < 0 || this.width < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            #region Save SmallEasy

            byte[] ByteImage1 = null;
            if (this.file1 == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage1 = FileHelper.ReadFully(this.file1.GetStream());
            this.cost = decimal.Parse(this.cost1);
            this.advance = decimal.Parse(this.advance1);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "SmallEasy",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage1,
                Alto = this.height,
                Ancho = this.width,
            };

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_teccaractController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save SmallMedium

            byte[] ByteImage2 = null;
            if (this.file2 == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage2 = FileHelper.ReadFully(this.file2.GetStream());

            this.apiService = new ApiService();
            this.cost = decimal.Parse(this.cost2);
            this.advance = decimal.Parse(this.advance2);
            if (this.cost < 0 || this.advance < 0)
            {
                await App.Current.MainPage.DisplayAlert(
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
                Imagen_Ejemplo = ByteImage2,
                Alto = this.height,
                Ancho = this.width,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save SmallHard

            byte[] ByteImage3 = null;
            if (this.file3 == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage3 = FileHelper.ReadFully(this.file3.GetStream());

            this.cost = decimal.Parse(this.cost3);
            this.advance = decimal.Parse(this.advance3);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "SmallHard",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage3,
                Alto = this.height,
                Ancho = this.width,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #endregion

            #region MediumSize
            if (string.IsNullOrEmpty(height2) || string.IsNullOrEmpty(Width2))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                return;
            }

            this.height = decimal.Parse(this.height2);
            this.width = decimal.Parse(this.width2);
            if (this.height < 0 || this.width < 0)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            #region Save MediumEasy

            byte[] ByteImage4 = null;
            if (this.file4 == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage4 = FileHelper.ReadFully(this.file4.GetStream());

            this.cost = decimal.Parse(this.cost4);
            this.advance = decimal.Parse(this.advance4);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
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
                Imagen_Ejemplo = ByteImage4,
                Alto = this.height,
                Ancho = this.width,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
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
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage5 = FileHelper.ReadFully(this.file5.GetStream());

            this.cost = decimal.Parse(this.cost5);
            this.advance = decimal.Parse(this.advance5);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "MediumMedium",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage5,
                Alto = this.height,
                Ancho = this.width,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save MediumHard

            byte[] ByteImage6 = null;
            if (this.file6 == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage6 = FileHelper.ReadFully(this.file6.GetStream());

            this.cost = decimal.Parse(this.cost6);
            this.advance = decimal.Parse(this.advance6);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "MediumHard",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage6,
                Alto = this.height,
                Ancho = this.width,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #endregion

            #region BigSize
            if (string.IsNullOrEmpty(height3) || string.IsNullOrEmpty(Width3))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.SizeError,
                    "Ok");
                return;
            }

            this.height = decimal.Parse(this.height3);
            this.width = decimal.Parse(this.width3);
            if (this.height < 0 || this.width < 0)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            #region Save BigEasy

            byte[] ByteImage7 = null;
            if (this.file7 == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage7 = FileHelper.ReadFully(this.file7.GetStream());

            this.cost = decimal.Parse(this.cost7);
            this.advance = decimal.Parse(this.advance7);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "BigEasy",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage7,
                Alto = this.height,
                Ancho = this.width,
            };
            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save BigMedium

            byte[] ByteImage8 = null;
            if (this.file8 == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage8 = FileHelper.ReadFully(this.file8.GetStream());

            this.cost = decimal.Parse(this.cost8);
            this.advance = decimal.Parse(this.advance8);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }
            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "BigMedium",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage8,
                Alto = this.height,
                Ancho = this.width,
            };
            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion

            #region Save BigHard

            byte[] ByteImage9 = null;
            if (this.file9 == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage9 = FileHelper.ReadFully(this.file9.GetStream());

            this.cost = decimal.Parse(this.cost9);
            this.advance = decimal.Parse(this.advance9);
            if (this.cost < 0 || this.advance < 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NegativeError,
                    "OK");
                return;
            }

            this.feature = new T_teccaract
            {
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Caract = "BigHard",
                Total_Aprox = this.cost,
                Costo_Cita = this.advance,
                Imagen_Ejemplo = ByteImage9,
                Alto = this.height,
                Ancho = this.width,
            };

            response = await this.apiService.Post(urlApi, prefix, controller, this.feature);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            #endregion 
            #endregion

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();
            MainViewModel.GetInstance().TecnicoHome = new TecnicoHomeViewModel(user);
            Application.Current.MainPage = new SNavigationPage(new TecnicoHomePage())
            {
                BarBackgroundColor = Color.FromRgb(20, 20, 20),
                BarTextColor = Color.FromRgb(200, 200, 200),
            };
        }
        #endregion
    }
}
