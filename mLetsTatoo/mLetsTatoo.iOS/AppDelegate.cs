using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfCarousel.XForms.iOS;


using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.SfSchedule.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using mLetsTatoo.ViewModels;
using mLetsTatoo.iOS.Controls;
using WindowsAzure.Messaging;
using PayPal.Forms.Abstractions;
using PayPal.Forms;

namespace mLetsTatoo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public SBNotificationHub Hub { get; set; }
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
global::Xamarin.Forms.Forms.Init();
SfPickerRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfCalendarRenderer.Init();
            SfCarouselRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfScheduleRenderer.Init();
            SfCheckBoxRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            NControls.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            var config = new PayPalConfiguration(PayPalEnvironment.Production, "AfV0uHgHfdRiSCM7jX786-0iatiiG-GtFwRRXe7L7xKQZYwTpbuAqGUvkeMpvbaXsWN_GOB1n69D5HYh")
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "Test Store",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://Letstattoo.com.mx/Terminos-y-Condiciones/",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://Letstattoo.com.mx/Terminos-y-Condiciones/",
                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                //ShippingAddressOption = ShippingAddressOption.None,
                // OPTIONAL - Language: Default languege for PayPal Plug-In
                //Language = "es",
                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                //PhoneCountryCode = "52",
            };

            CrossPayPalManager.Init(config);

            LoadApplication(new App());

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
                UIRemoteNotificationType notificationType = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationType);
            }

            return base.FinishedLaunching(app, options);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Hub = new SBNotificationHub(Constants.ConnectionString, Constants.NotificationHubName);
            Hub.UnregisterAllAsync(deviceToken, (error) =>
            {
            if (error != null)
            {
                Console.WriteLine("Error calling Unregister: {0}", error.ToString());
                return;
            }

            var tags_list = new List<string>() { };
            var mainviewModel = MainViewModel.GetInstance();
            if (mainviewModel.Login != null)
                {
                    var userId = mainviewModel.Login.user.Id_usuario;
                    tags_list.Add("userId:" + userId);
                }

                var tags = new NSSet(tags_list.ToArray());
                Hub.RegisterNativeAsync(deviceToken, tags, (errorCallback) =>
                {
                    if (errorCallback != null)
                        Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                });
            });            
        }
        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            ProcessNotification(userInfo, false);
        }
        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            if(null != options && options.ContainsKey(new NSString("aps")))
            {
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;
                string alert = string.Empty;
                string type = string.Empty;
                string notification = string.Empty;

                if(aps.ContainsKey(new NSString("alert")))
                {
                    alert = (aps[new NSString("alert")] as NSString).ToString();
                }

                if (!fromFinishedLaunching)
                {
                    var avAlert = new UIAlertView("LetsTattoo", alert, null, "Ok", null);
                    avAlert.Show();
                }
                
            }
        }
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "Ok", null);
        }
    }
}
