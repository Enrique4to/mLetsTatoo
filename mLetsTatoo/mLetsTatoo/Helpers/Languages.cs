namespace mLetsTatoo.Helpers
{
    using Interfaces;
    using Resources;
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
        public static string OlvidoPassword
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
        public static string Next
        {
            get { return Resource.Next; }
        }
        public static string Birthdate
        {
            get { return Resource.Birthdate; }
        }
        public static string BirthdateError
        {
            get { return Resource.BirthdateError; }
        }
        public static string UserError
        {
            get { return Resource.UserError; }
        }
        public static string PasswordError
        {
            get { return Resource.PasswordError; }
        }
        public static string ConfirmPasswordError
        {
            get { return Resource.ConfirmPasswordError; }
        }
        public static string Name
        {
            get { return Resource.Name; }
        }
        public static string Lastname
        {
            get { return Resource.Lastname; }
        }
        public static string Email
        {
            get { return Resource.Email; }
        }
        public static string ConfirmPassword
        {
            get { return Resource.ConfirmPassword; }
        }
        public static string EnterPhone
        {
            get { return Resource.EnterPhone; }
        }
        public static string PersonalData
        {
            get { return Resource.PersonalData; }
        }
        public static string UserData
        {
            get { return Resource.UserData; }
        }
        public static string RegPersonal
        {
            get { return Resource.RegPersonal; }
        }
        public static string RegAccount
        {
            get { return Resource.RegAccount; }
        }
        public static string NameError
        {
            get { return Resource.NameError; }
        }
        public static string LastnameError
        {
            get { return Resource.LastnameError; }
        }
        public static string EmailError
        {
            get { return Resource.EmailError; }
        }
        public static string PhoneError
        {
            get { return Resource.PhoneError; }
        }
        public static string AgeError
        {
            get { return Resource.AgeError; }
        }
        public static string MatchPasswordError
        {
            get { return Resource.MatchPasswordError; }
        }
        public static string UserExistError
        {
            get { return Resource.UserExistError; }
        }
        public static string EmailExistError
        {
            get { return Resource.EmailExistError; }
        }
        public static string AccountBloqued
        {
            get { return Resource.AccountBloqued; }
        }
        public static string WhereTakePicture
        {
            get { return Resource.WhereTakePicture; }
        }
        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }
        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }
        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string EditUser
        {
            get { return Resource.EditUser; }
        }
        public static string ErrorTypeUser
        {
            get { return Resource.ErrorTypeUser; }
        }
        public static string ChangePassword
        {
            get { return Resource.ChangePassword; }
        }
        public static string NewPassword
        {
            get { return Resource.NewPassword; }
        }
        public static string CurrentPassword
        {
            get { return Resource.CurrentPassword; }
        }
        public static string Save
        {
            get { return Resource.Save; }
        }
        public static string ChangeEmail
        {
            get { return Resource.ChangeEmail; }
        }
        public static string ChangePersonal
        {
            get { return Resource.ChangePersonal; }
        }
        public static string NewPasswordError
        {
            get { return Resource.NewPasswordError; }
        }
        public static string CurrentPasswordError
        {
            get { return Resource.CurrentPasswordError; }
        }
        public static string NoCurrentPassword
        {
            get { return Resource.NoCurrentPassword; }
        }
        public static string UserInfo
        {
            get { return Resource.UserInfo; }
        }
        public static string Study
        {
            get { return Resource.Study; }
        }
        public static string Artists
        {
            get { return Resource.Artists; }
        }
        public static string Images
        {
            get { return Resource.Images; }
        }
        public static string BranchOffices
        {
            get { return Resource.BranchOffices; }
        }
        public static string BranchOffice
        {
            get { return Resource.BranchOffice; }
        }
        public static string AboutStudie
        {
            get { return Resource.AboutStudie; }
        }
        public static string Search
        {
            get { return Resource.Search; }
        }
        public static string SearchStudios
        {
            get { return Resource.SearchStudios; }
        }
        public static string SearchArtists
        {
            get { return Resource.SearchArtists; }
        }
        public static string SearchAppointment
        {
            get { return Resource.SearchAppointment; }
        }
        public static string SelectArtist
        {
            get { return Resource.SelectArtist; }
        }
        public static string AppointmentSchedule
        {
            get { return Resource.AppointmentSchedule; }
        }
        
    }
}
