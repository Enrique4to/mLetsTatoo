namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using Views;
    using Xamarin.Forms;
    public class LocalViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion
        #region Attributes
        private string nombreCompleto;
        private byte[] byteImage;
        private ImageSource imageSource;
        private MediaFile file;
        private bool isRefreshing;
        private bool isRunning;
        public List<T_clientes> listClientes;
        public T_clientes cliente;
        public T_usuarios user;
        public T_locales local;
        #endregion
        #region Properties
        public string Nombre { get; set; }

        public T_locales Local
        {
            get { return this.local; }
            set { SetValue(ref this.local, value); }
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
        #endregion
        #region Constructors
        public LocalViewModel(T_locales local,T_usuarios user, T_clientes cliente)
        {
            this.local = local;
            this.user = user;
            this.cliente = cliente;
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.LoadUser();            
        }
        #endregion

        #region Commands
        #endregion

        #region Methods
        private void LoadUser()
        {
            this.Nombre = this.local.Nombre;

        }
        #endregion

    }
}
