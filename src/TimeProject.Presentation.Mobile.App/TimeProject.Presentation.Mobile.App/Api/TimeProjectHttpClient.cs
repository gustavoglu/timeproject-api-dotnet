using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Presentation.Mobile.App.Models;
using Xamarin.Essentials;

namespace TimeProject.Presentation.Mobile.App.Api
{
    public class TimeProjectHttpClient
    {
        public HttpClient Client { get; private set; }
 
        public TimeProjectHttpClient()
        {
            var handlerSetToken = new HttpClientHandlerSetToken() { InnerHandler = new HttpClientHandler() };
            Client = new HttpClient(handlerSetToken) { BaseAddress = new Uri("http://192.168.15.2:5000/api/"), Timeout = TimeSpan.FromSeconds(30) };
        }

        private async Task<string> GetUserToken()
        {
            string userParamsJson = await SecureStorage.GetAsync("user-params");
            if (string.IsNullOrEmpty(userParamsJson)) return null;
            UserParamsResponse userParams = JsonConvert.DeserializeObject<UserParamsResponse>(userParamsJson);
            return userParams.TokenAccess;
        }
    }
}
