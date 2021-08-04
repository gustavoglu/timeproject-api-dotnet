using System;
using System.Net.Http;

namespace TimeProject.Infra.ApiClient.Client
{
    public class TimeProjectHttpClient
    {
        public HttpClient Client { get; private set; }
        public TimeProjectHttpClient(string baseUrl)
        {
            Client = new HttpClient() { BaseAddress = new Uri(baseUrl) };
        }
    }
}
