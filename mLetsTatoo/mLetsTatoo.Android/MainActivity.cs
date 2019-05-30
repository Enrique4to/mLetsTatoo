using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using mLetsTatoo.Droid.Controls;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using System;

[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
namespace mLetsTatoo.Droid
{

    [Activity(Label = "LetsTatoo", 
        Icon = "@drawable/ic_launcher", 
        Theme = "@style/MainTheme", 
        MainLauncher = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        #region Singleton
        private static MainActivity instance;
        public static MainActivity GetInstance()
        {
            if(instance == null)
            {
                instance = new MainActivity();
            }
            return instance;
        }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //inicialize plugin
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            NControls.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            var config = new PayPalConfiguration(PayPalEnvironment.Production, "AWEDFUuv1XzylbHqV2HWYdhLYc7960aIIsGP33FvbQw8lwotAPTODKekDZVmw5Xn6TDcu1lS2-kzBn_p")
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "LetsTattoo",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://Letstattoo.com.mx/Terminos-y-Condiciones/",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://Letstattoo.com.mx/Terminos-y-Condiciones/",
                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                //ShippingAddressOption = ShippingAddressOption.None,
                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "es",
                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "52",
            };

            CrossPayPalManager.Init(config, this);

            LoadApplication(new App());

        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(
                requestCode,
                permissions,
                grantResults);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }
        //public override void OnBackPressed()
        //{

        //    //if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
        //    //{

        //    //}
        //    //else
        //    //{
        //    //}
        //}
    }

}