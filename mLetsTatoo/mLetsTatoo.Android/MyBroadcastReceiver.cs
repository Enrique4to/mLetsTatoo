﻿
[assembly: Android.App.Permission(Name ="@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: Android.App.UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: Android.App.UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: Android.App.UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: Android.App.UsesPermission(Name = "android.permission.INTERNET")]
[assembly: Android.App.UsesPermission(Name = "android.permission.WAKE_LOCK")]
namespace mLetsTatoo.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Android.App;
    using Android.Content;
    using Android.Util;
    using Gcm.Client;
    using ViewModels;
    using WindowsAzure.Messaging;

    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { Constants.SenderID };
        public const string TAG = "MyBroadcastReceiver-GCM";
    }
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        #region Properties
        public NotificationHub Hub { get; set; }
        public static string RegistrationId { get; private set; }
        #endregion
        #region Methods
        public PushHandlerService() : base(Constants.SenderID)
        {
            Log.Info(MyBroadcastReceiver.TAG, "PushHandlerService() constructor");
        }

        protected override void OnMessage(Context contect, Intent intent)
        {
            Log.Info(MyBroadcastReceiver.TAG, "GCM Message Received!");
            var msg = new StringBuilder();
            if(intent !=null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            var message = intent.Extras.GetString("Message");
            var type = intent.Extras.GetString("Type");

            if (!string.IsNullOrEmpty(message))
            {
                var notification = intent.Extras.GetString("Notification");
                createNotification("LetsTattoo", message);
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(MyBroadcastReceiver.TAG, "Recoverable Error: " + errorId);
            return base.OnRecoverableError(context,errorId);
        }
        protected override void OnError(Context context, string errorId)
        {
            Log.Error(MyBroadcastReceiver.TAG, "GCO Error: " + errorId);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
            RegistrationId = registrationId;

            Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString, context);
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception EX)
            {
                Log.Error(MyBroadcastReceiver.TAG, EX.Message);
            }

            var tags = new List<string>() { };
            var mainviewModel = MainViewModel.GetInstance();
            if(mainviewModel.Login.user != null)
            {
                var userID = mainviewModel.Login.user.Id_usuario;
                var type = mainviewModel.Login.user.Tipo;
                tags.Add("userID:" + userID);
                tags.Add("Type:" + type);
            }
            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {

                Log.Error(MyBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "Gcm Unregistered:" + registrationId);
            createNotification("LetsTattoo", "The Ddevice has been unregistered!");
        }
        void createNotification(String title, String desc)
        {
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(this, typeof(MainActivity));
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);
            notification.Flags = NotificationFlags.AutoCancel;
            notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));
            notificationManager.Notify(1, notification);
            dialogNotify(title, desc);
        }
        protected void dialogNotify(String title, String message)
        {
            var mainActivity = MainActivity.GetInstance();
            mainActivity.RunOnUiThread(() =>
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(mainActivity);
                AlertDialog alert = dlg.Create();
                alert.SetIcon(Resource.Drawable.ic_launcher);
                alert.SetMessage(message);
                alert.Show();
            });
        }
        #endregion
    }
}