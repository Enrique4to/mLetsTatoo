﻿namespace mLetsTatoo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Helpers;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Popups.ViewModel;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using ViewModels;
    using Xamarin.Forms;
    using Microsoft.Azure.NotificationHubs;
    using mLetsTatoo.Common;
    using Org.Apache.Http.Client.Methods;

    public class ApiService
    {
        private static NotificationHubClient hub;
        #region Methods
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.ErrorConfigInternet,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "https://mletstattooapi.azurewebsites.net/");

            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.ErrorInternet,
                };
            }
            return new Response
            {
                IsSuccess = true,
                Message = "OK",
            };
        }        
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response

                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<Response> Get<T>(string urlBase, string prefix, string controller, int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(result);
                return new Response

                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(result);
                return new Response

                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<Response> Put<T>(string urlBase, string prefix, string controller, T model, int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.ToString(),
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response

                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                return new Response

                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async void StartActivityPopup()
        {
            MainViewModel.GetInstance().ActivityIndicatorPopup = new ActivityIndicatorPopupViewModel();
            MainViewModel.GetInstance().ActivityIndicatorPopup.IsRunning = true;
            await Application.Current.MainPage.Navigation.PushPopupAsync(new ActivityIndicatorPopupPage());
        }
        public async void EndActivityPopup()
        {
            var daStack = Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count();
            if (daStack > 0)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }
        }

        public byte[] GetImageFromFile(string fileName)
        {
            //var applicationTypeInfo = Application.Current.GetType().GetTypeInfo();
            var assembly = GetType().GetTypeInfo().Assembly;
            byte[] buffer = null;
            using (var stream = assembly.GetManifestResourceStream(fileName))
            {
                if (stream != null)
                {
                    long length = stream.Length;
                    buffer = new byte[length];
                    stream.Read(buffer, 0, (int)length);
                }
            }

            return buffer;
        }
        public async void SendNotificationAsync(string message, int to, string fromName)
        {
            string userTag = $"userID:{to}";                

            hub = NotificationHubClient.CreateClientFromConnectionString(Constants.ListenConnectionString, Constants.NotificationHubName);
            var notif = "{ \"data\" : {\"Message\":\"" + "From " + fromName + ": " + message + "\"}}";
            NotificationOutcome outcome = await hub.SendFcmNativeNotificationAsync(notif, userTag);
        }  
        #endregion
    }
}
