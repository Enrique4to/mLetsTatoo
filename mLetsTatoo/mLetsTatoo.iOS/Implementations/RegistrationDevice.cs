

[assembly: Xamarin.Forms.Dependency(typeof(mLetsTatoo.iOS.Implementations.RegistrationDevice))]
namespace mLetsTatoo.iOS.Implementations
{
    using Interfaces;
    using Foundation;
    using UIKit;
    public class RegistrationDevice : IRegisterDevice
    {
        #region Methods
        public void RegisterDevice()
        {
            if(UIDevice.CurrentDevice.CheckSystemVersion(8,0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationType =
                    UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;

                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationType);
            }
        }
        #endregion
    }
}