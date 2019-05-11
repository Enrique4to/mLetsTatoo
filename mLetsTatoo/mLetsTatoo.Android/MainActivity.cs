﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using mLetsTatoo.Droid.Controls;
using Plugin.CurrentActivity;
using Plugin.Permissions;

[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
namespace mLetsTatoo.Droid
{

    [Activity(Label = "LetsTatoo", 
        Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //inicialize plugin
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            NControls.Init();
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