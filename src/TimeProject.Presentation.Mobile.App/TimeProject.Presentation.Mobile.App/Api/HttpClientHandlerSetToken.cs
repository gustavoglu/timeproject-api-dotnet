using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Presentation.Mobile.App.Models;
using Xamarin.Essentials;

namespace TimeProject.Presentation.Mobile.App.Api
{
    public class HttpClientHandlerSetToken : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var userParamsJson = await SecureStorage.GetAsync("user-params");
            if (!string.IsNullOrEmpty(userParamsJson))
            {
                var userParams = JsonConvert.DeserializeObject<UserParamsResponse>(userParamsJson);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", userParams.TokenAccess);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
