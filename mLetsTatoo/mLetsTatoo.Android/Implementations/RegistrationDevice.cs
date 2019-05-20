

[assembly: Xamarin.Forms.Dependency(typeof(mLetsTatoo.Droid.Implementations.RegistrationDevice))]
namespace mLetsTatoo.Droid.Implementations
{
    using Interfaces;
    using Gcm.Client;
    using Android.Util;
    public class RegistrationDevice : IRegisterDevice
    {
        #region Methods
        public void RegisterDevice()
        {
            var mainActivity = MainActivity.GetInstance();
            GcmClient.CheckDevice(mainActivity);
            GcmClient.CheckManifest(mainActivity);

            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(mainActivity, Droid.Constants.SenderID);
        }
        #endregion
    }
}