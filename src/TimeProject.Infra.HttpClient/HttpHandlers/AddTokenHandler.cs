using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TimeProject.Infra.ApiClient.Interfaces;

namespace TimeProject.Infra.ApiClient.HttpHandlers
{
    public class AddTokenHandler : DelegatingHandler
    {
        private readonly IUserTokenService _userTokenService;

        public AddTokenHandler(IUserTokenService userTokenService)
        {
            _userTokenService = userTokenService;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            string token = _userTokenService.GetToken();
            if(!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            

            return base.SendAsync(request, cancellationToken);
        }
    }
}
