namespace mLetsTatoo.Services
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.MobileServices;
    using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
    using Microsoft.WindowsAzure.MobileServices.Sync;
    using Models;
    using Xamarin.Forms;

    public partial class SyncDBManager
    {
        static SyncDBManager defaultInstance = new SyncDBManager();
        IMobileServiceClient client;
        IMobileServiceSyncTable<T_usuarios> usuariosTable;
        private SyncDBManager()
        {
            try
            {
                var URI = $"{Application.Current.Resources["UrlAPI"].ToString()}{Application.Current.Resources["UrlPrefix"].ToString()}";
                this.client = new MobileServiceClient(URI);
                var store = new MobileServiceSQLiteStore("localstore.db");
                store.DefineTable<T_usuarios>();
                this.client.SyncContext.InitializeAsync(store);
                this.usuariosTable = client.GetSyncTable<T_usuarios>();
            }
            catch (System.Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }
        public static SyncDBManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.usuariosTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allUsers",
                    this.usuariosTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }

    }
}
