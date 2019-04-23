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
        public static string Date
        {
            get { return Resource.Date; }
        }
        public static string Time
        {
            get { return Resource.Time; }
        }
        public static string AppointmentWho
        {
            get { return Resource.AppointmentWho; }
        }
        public static string AppointmentWhen
        {
            get { return Resource.AppointmentWhen; }
        }
        public static string FeaturesArt
        {
            get { return Resource.FeaturesArt; }
        }
        public static string Size
        {
            get { return Resource.Size; }
        }
        public static string Complexity
        {
            get { return Resource.Complexity; }
        }
        public static string Small
        {
            get { return Resource.Small; }
        }
        public static string MediumSize
        {
            get { return Resource.MediumSize; }
        }
        public static string Big
        {
            get { return Resource.Big; }
        }
        public static string Easy
        {
            get { return Resource.Easy; }
        }
        public static string MediumComplexity
        {
            get { return Resource.MediumComplexity; }
        }
        public static string Hard
        {
            get { return Resource.Hard; }
        }
        public static string DescribeArt
        {
            get { return Resource.DescribeArt; }
        }
        public static string AddArtImage
        {
            get { return Resource.AddArtImage; }
        }
        public static string SelectedArtistError
        {
            get { return Resource.SelectedArtistError; }
        }
        public static string DateError
        {
            get { return Resource.DateError; }
        }
        public static string DescribeArtError
        {
            get { return Resource.DescribeArtError; }
        }
        public static string ApproximateCost
        {
            get { return Resource.ApproximateCost; }
        }
        public static string AppointmentCost
        {
            get { return Resource.AppointmentCost; }
        }
        public static string IntroduceActivationCode
        {
            get { return Resource.IntroduceActivationCode; }
        }
        public static string ActivationCode
        {
            get { return Resource.ActivationCode; }
        }
        public static string ActivationCodeError
        {
            get { return Resource.ActivationCodeError; }
        }
        public static string Notice
        {
            get { return Resource.Notice; }
        }
        public static string CompleteFeatures
        {
            get { return Resource.CompleteFeatures; }
        }
        public static string FillFeatures
        {
            get { return Resource.FillFeatures; }
        }
        public static string Cost
        {
            get { return Resource.Cost; }
        }
        public static string Advance
        {
            get { return Resource.Advance; }
        }
        public static string Done
        {
            get { return Resource.Done; }
        }
        public static string CostError
        {
            get { return Resource.CostError; }
        }
        public static string AdvanceError
        {
            get { return Resource.AdvanceError; }
        }
        public static string ExampleImageError
        {
            get { return Resource.ExampleImageError; }
        }
        public static string Example
        {
            get { return Resource.Example; }
        }
        public static string NegativeError
        {
            get { return Resource.NegativeError; }
        }
        public static string Height
        {
            get { return Resource.Height; }
        }
        public static string Width
        {
            get { return Resource.Width; }
        }
        public static string SizeError
        {
            get { return Resource.SizeError; }
        }
        public static string MaximunSize
        {
            get { return Resource.MaximunSize; }
        }
        public static string EstimatedTime
        {
            get { return Resource.EstimatedTime; }
        }
        public static string SelectEstimatedTime
        {
            get { return Resource.SelectEstimatedTime; }
        }
        public static string EstimatedTimeError
        {
            get { return Resource.EstimatedTimeError; }
        }
        public static string YourAppointment
        {
            get { return Resource.YourAppointment; }
        }
        public static string HasBeenCreated
        {
            get { return Resource.HasBeenCreated; }
        }
        public static string At
        {
            get { return Resource.At; }
        }
        public static string WithThe_Artist
        {
            get { return Resource.WithThe_Artist; }
        }
        public static string TryToBe
        {
            get { return Resource.TryToBe; }
        }
        public static string QuickAppointment
        {
            get { return Resource.QuickAppointment; }
        }
        public static string PersonalizedAppointment
        {
            get { return Resource.PersonalizedAppointment; }
        }
        public static string TempJobMessageSent
        {
            get { return Resource.TempJobMessageSent; }
        }
        public static string TempJobAnswerMessage
        {
            get { return Resource.TempJobAnswerMessage; }
        }
        public static string Tattoo
        {
            get { return Resource.Tattoo; }
        }
        public static string Personalized
        {
            get { return Resource.Personalized; }
        }
        public static string NewsFeed
        {
            get { return Resource.NewsFeed; }
        }
        public static string Profile
        {
            get { return Resource.Profile; }
        }
        public static string NewDate
        {
            get { return Resource.NewDate; }
        }
        public static string Appointment
        {
            get { return Resource.Appointment; }
        }
        public static string Subject
        {
            get { return Resource.Subject; }
        }
        public static string Hour
        {
            get { return Resource.Hour; }
        }
        public static string AppointmentWhere
        {
            get { return Resource.AppointmentWhere; }
        }
        public static string Phone
        {
            get { return Resource.Phone; }
        }
        public static string Reference
        {
            get { return Resource.Reference; }
        }
        public static string SubTotal
        {
            get { return Resource.SubTotal; }
        }
        public static string Remaining
        {
            get { return Resource.Remaining; }
        }
        public static string YourArt
        {
            get { return Resource.YourArt; }
        }
        public static string Art
        {
            get { return Resource.Art; }
        }
        public static string CustomerArt
        {
            get { return Resource.CustomerArt; }
        }
        public static string AddComment
        {
            get { return Resource.AddComment; }
        }
        public static string DeleteComment
        {
            get { return Resource.DeleteComment; }
        }
        public static string Yes
        {
            get { return Resource.Yes; }
        }
        public static string No
        {
            get { return Resource.No; }
        }
    }
}
