namespace mLetsTatoo.Infraestructure
{
    using ViewModels;
    public class InstanceLocator
    {
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion
        #region Construntors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion

    }
}
