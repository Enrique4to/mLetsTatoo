namespace mLetsTatoo.Helpres
{
    using Interfaces;
    using Resourses;
    using Xamarin.Forms;
    public class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string IntroducirUsuario
        {
            get { return Resource.IntroducirUsuario; }
        }
        public static string ErrorUsuarioyPassword
        {
            get { return Resource.ErrorUsuarioyPassword; }
        }
        public static string IntroducirPasword
        {
            get { return Resource.IntroducirPasword; }
        }
        public static string ErrorConfigInternet
        {
            get { return Resource.ErrorConfigInternet; }
        }
        public static string ErrorInternet
        {
            get { return Resource.ErrorInternet; }
        }
        public static string Usuario
        {
            get { return Resource.Usuario; }
        }
        public static string Password
        {
            get { return Resource.Password; }
        }
        public static string Recordarme
        {
            get { return Resource.Recordarme; }
        }
        public static string Olvidopassword
        {
            get { return Resource.OlvidoPassword; }
        }
        public static string Inicio
        {
            get { return Resource.Inicio; }
        }
        public static string Registrate
        {
            get { return Resource.Registrate; }
        }
        public static string InicioSesion
        {
            get { return Resource.InicioSesion; }
        }
    }
}
