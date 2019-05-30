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
        public static string TimeError
        {
            get { return Resource.TimeError; }
        }
        public static string DescribeArtError
        {
            get { return Resource.DescribeArtError; }
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
        public static string FillSmallFeatures
        {
            get { return Resource.FillSmallFeatures; }
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
        public static string PhoneLenghtError
        {
            get { return Resource.PhoneLenghtError; }
        }
        public static string Budget
        {
            get { return Resource.Budget; }
        }
        public static string Message
        {
            get { return Resource.Message; }
        }
        public static string BudgetSentMessage
        {
            get { return Resource.BudgetSentMessage; }
        }
        public static string AppointmentType
        {
            get { return Resource.AppointmentType; }
        }
        public static string QuickDetails
        {
            get { return Resource.QuickDetails; }
        }
        public static string PersonalizedDetails
        {
            get { return Resource.PersonalizedDetails; }
        }
        public static string ScheduleError
        {
            get { return Resource.ScheduleError; }
        }
        public static string ArtError
        {
            get { return Resource.ArtError; }
        }
        public static string ChangeDate
        {
            get { return Resource.ChangeDate; }
        }
        public static string TheDateChanged
        {
            get { return Resource.TheDateChanged; }
        }
        public static string HasBeenChanged
        {
            get { return Resource.HasBeenChanged; }
        }
        public static string WillBeChanged
        {
            get { return Resource.WillBeChanged; }
        }
        public static string To
        {
            get { return Resource.To; }
        }
        public static string CustomerAccept
        {
            get { return Resource.CustomerAccept; }
        }
        public static string ChangeDateAlert
        {
            get { return Resource.ChangeDateAlert; }
        }
        public static string ChangeDateTecMessage
        {
            get { return Resource.ChangeDateTecMessage; }
        }
        public static string TheNewDate
        {
            get { return Resource.TheNewDate; }
        }
        public static string ChangeDateWantAccept
        {
            get { return Resource.ChangeDateWantAccept; }
        }
        public static string Total
        {
            get { return Resource.Total; }
        }
        public static string CancelDate
        {
            get { return Resource.CancelDate; }
        }
        public static string Comments
        {
            get { return Resource.Comments; }
        }
        public static string CancelDateMessage
        {
            get { return Resource.CancelDateMessage; }
        }
        public static string SelectHorario
        {
            get { return Resource.SelectHorario; }
        }
        public static string LunVie
        {
            get { return Resource.LunVie; }
        }
        public static string Sabado
        {
            get { return Resource.Sabado; }
        }
        public static string Domingo
        {
            get { return Resource.Domingo; }
        }
        public static string HorarioError
        {
            get { return Resource.HorarioError; }
        }
        public static string SelectLunVie
        {
            get { return Resource.SelectLunVie; }
        }
        public static string SelectSabado
        {
            get { return Resource.SelectSabado; }
        }
        public static string SelectDomingo
        {
            get { return Resource.SelectDomingo; }
        }
        public static string CheckInTime
        {
            get { return Resource.CheckInTime; }
        }
        public static string CheckOutTime
        {
            get { return Resource.CheckOutTime; }
        }
        public static string CheckInTimeToEat
        {
            get { return Resource.CheckInTimeToEat; }
        }
        public static string CheckOutTimeToEat
        {
            get { return Resource.CheckOutTimeToEat; }
        }
        public static string HorarioComida
        {
            get { return Resource.HorarioComida; }
        }
        public static string FillComplexity
        {
            get { return Resource.FillComplexity; }
        }
        public static string LunVieActError
        {
            get { return Resource.LunVieActError; }
        }
        public static string SabActError
        {
            get { return Resource.SabActError; }
        }
        public static string DomActError
        {
            get { return Resource.DomActError; }
        }
        public static string LVRangoHorarioError1
        {
            get { return Resource.LVRangoHorarioError1; }
        }
        public static string SRangoHorarioError1
        {
            get { return Resource.SRangoHorarioError1; }
        }
        public static string DRangoHorarioError1
        {
            get { return Resource.DRangoHorarioError1; }
        }
        public static string RangoHorarioError
        {
            get { return Resource.RangoHorarioError; }
        }
        public static string RangoHorarioComidaError
        {
            get { return Resource.RangoHorarioComidaError; }
        }
        public static string FillFeatures
        {
            get { return Resource.FillFeatures; }
        }
        public static string AddPublication
        {
            get { return Resource.AddPublication; }
        }
        public static string CancelUserDateMessage
        {
            get { return Resource.CancelUserDateMessage; }
        }
        public static string PaymentMethod
        {
            get { return Resource.PaymentMethod; }
        }
        public static string Date48Hours
        {
            get { return Resource.Date48Hours; }
        }
        public static string DateBeforeTodayError
        {
            get { return Resource.DateBeforeTodayError; }
        }
        public static string AddNewDateMessage
        {
            get { return Resource.AddNewDateMessage; }
        }
        public static string PositiveBalance
        {
            get { return Resource.PositiveBalance; }
        }
        public static string NegativeBalance
        {
            get { return Resource.NegativeBalance; }
        }
        public static string RetainedBalance
        {
            get { return Resource.RetainedBalance; }
        }
        public static string CashMethodMessage
        {
            get { return Resource.CashMethodMessage; }
        }
        public static string YouWishContiniue
        {
            get { return Resource.YouWishContinue; }
        }
        public static string TypeCashDate
        {
            get { return Resource.TypeCashDate; }
        }
        public static string PagoTecnicoConcept1
        {
            get { return Resource.PagoTecnicoConcept1; }
        }
        public static string PagoTecnicoConcept2
        {
            get { return Resource.PagoTecnicoConcept2; }
        }
        public static string StartArt
        {
            get { return Resource.StartArt; }
        }
        public static string AcceptTerminos
        {
            get { return Resource.AcceptTerminos; }
        }
        public static string TermsConditions
        {
            get { return Resource.TermsConditions; }
        }
        public static string Options
        {
            get { return Resource.Options; }
        }
        public static string EditFeaturesOptions
        {
            get { return Resource.EditFeaturesOptions; }
        }
        public static string OpEditSchedulerOptionstions
        {
            get { return Resource.EditSchedulerOptions; }
        }
        public static string EditUserOptions
        {
            get { return Resource.EditUserOptions; }
        }
        public static string TheArtist
        {
            get { return Resource.TheArtist; }
        }
        public static string TheClient
        {
            get { return Resource.TheClient; }
        }
        public static string NotifNewQuickApp
        {
            get { return Resource.NotifNewQuickApp; }
        }
        public static string NotifNewPersonalizedApp
        {
            get { return Resource.NotifNewPersonalizedApp; }
        }
        public static string NotifAcceptBudget
        {
            get { return Resource.NotifAcceptBudget; }
        }
        public static string NotifChageDate
        {
            get { return Resource.NotifChageDate; }
        }
        public static string NotifNewApp
        {
            get { return Resource.NotifNewApp; }
        }
        public static string NotifNewComment
        {
            get { return Resource.NotifNewComment; }
        }
        public static string NotifAccepChangeDate
        {
            get { return Resource.NotifAccepChangeDate; }
        }
        public static string NotifRefuseChangeDate1
        {
            get { return Resource.NotifRefuseChangeDate1; }
        }
        public static string NotifRefuseChangeDate2
        {
            get { return Resource.NotifRefuseChangeDate2; }
        }
        public static string NotifArtCanceled
        {
            get { return Resource.NotifArtCanceled; }
        }
        public static string NotifArtStart
        {
            get { return Resource.NotifArtStart; }
        }
        public static string NotifFinishArt
        {
            get { return Resource.NotifFinishArt; }
        }
        public static string NotifMessagePersonalized
        {
            get { return Resource.NotifMessagePersonalized; }
        }
        public static string BankAccount
        {
            get { return Resource.BankAccount; }
        }
        public static string AccountCard
        {
            get { return Resource.AccountCard; }
        }
        public static string BankAccountInformation
        {
            get { return Resource.BankAccountInformation; }
        }
        public static string BankAccountUserName
        {
            get { return Resource.BankAccountUserName; }
        }
        public static string SelectBank
        {
            get { return Resource.SelectBank; }
        }
        public static string MaximunGetMoney
        {
            get { return Resource.MaximunGetMoney; }
        }
        public static string ErrorGetMoney
        {
            get { return Resource.ErrorGetMoney; }
        }
        public static string ErrorNoBankAccount
        {
            get { return Resource.ErrorNoBankAccount; }
        }
        public static string PaypalComission
        {
            get { return Resource.PaypalComission; }
        }
    }
}
