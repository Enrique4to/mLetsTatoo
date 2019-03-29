namespace mLetsTatoo.ViewModels
{
    using System.IO;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class TecnicoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes        
        private string nombreCompleto;
        private byte[] byteImage;
        private ImageSource imageSource;
        private bool isRunning;
        public T_clientes cliente;
        public T_usuarios user;
        public T_tecnicos tecnico;
        #endregion

        #region Properties
        public T_clientes Cliente
        {
            get { return this.cliente; }
            set { SetValue(ref this.cliente, value); }
        }
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public T_tecnicos Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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
        public TecnicoViewModel(T_tecnicos tecnico, T_usuarios user, T_clientes cliente)
        {
            this.user = user;
            this.cliente = cliente;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.LoadTecnico();
        }
        #endregion

        #region Commands
        #endregion
        
        #region Methods
        private void LoadTecnico()
        {
            this.NombreCompleto = $"{this.tecnico.Nombre} {this.tecnico.Apellido1}";

            if (cliente.F_Perfil != null)
            {
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.tecnico.F_Perfil));
            }
            else
            {
                this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
        }
        #endregion
    }
}
