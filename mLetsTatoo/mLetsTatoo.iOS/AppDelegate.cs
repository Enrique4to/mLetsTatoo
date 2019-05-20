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
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfCalendarRenderer.Init();
            SfCarouselRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfScheduleRenderer.Init();
            SfCheckBoxRenderer.Init();
            LoadApplication(new App());
            Rg.Plugins.Popup.Popup.Init();
            NControls.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
