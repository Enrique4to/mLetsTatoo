namespace mLetsTatoo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Helpres;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    

    public class ApiService
    {
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
                "https://mletstatooapi.azurewebsites.net");

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

        public async Task<Response> GetList<T>(
            string urlBase,
            string prefix,
            string controller)
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
        #endregion
    }
}
