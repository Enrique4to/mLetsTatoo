namespace mLetsTatoo.Popups.ViewModel
{
    using ViewModels;

    public class StartPopupViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public StartPopupViewModel()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            MainViewModel.GetInstance().Login.LoadLists();
        }
        #endregion

        #region Methods

        #endregion


    }
}
