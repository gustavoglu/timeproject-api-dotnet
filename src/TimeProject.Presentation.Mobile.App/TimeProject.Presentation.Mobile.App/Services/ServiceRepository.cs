using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeProject.Presentation.Mobile.App.Models;

namespace TimeProject.Presentation.Mobile.App.Services
{
    public class ServiceRepository
    {
        public HttpClient Client { get; private set; }
        public ServiceRepository()
        {
            Client = App.ApiClient;
        }

        protected ApiResponse<T> GetResponse<T>(Exception exception)
        {
            return new ApiResponse<T>(false, default,
                            new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("App Error", exception.Message) });
        }

        protected async Task<ApiResponse<T>> GetResponse<T>(HttpResponseMessage response)
        {
            try
            {
                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<ApiResponse<T>>(content);

                var desError = JsonConvert.DeserializeObject<ApiResponse<List<KeyValuePair<string, string>>>>(content);
                return new ApiResponse<T>(false, default, desError.Data);
            }
            catch (Exception e)
            {
                return new ApiResponse<T>(false, default, new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("App Error", e.Message) });
            }
        }
    }
}
