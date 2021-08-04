using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Presentation.Mobile.App.Models;
using Xamarin.Essentials;

namespace TimeProject.Presentation.Mobile.App.Services
{
    public class AuthService : ServiceRepository
    {
        public const string USERPARAMSKEYSECURESTORAGE = "user-params";
        public AuthService()
        {
        }

        public async Task<ApiResponse<object>> SignUp(string tenanty, string email, string name, string password, string confirmPassword)
        {
            try
            {
                var res = await Client.PostAsJsonAsync("user/register", new { tenanty, email, name, password, confirmPassword });
                return await GetResponse<object>(res);
            }
            catch (Exception e)
            {
                return GetResponse<object>(e);
            }
        }

        public async Task<ApiResponse<UserParamsResponse>> SignIn(string tenanty, string email, string password)
        {
            try
            {
                var res = await Client.PostAsJsonAsync("user/token", new { tenanty, email, password });
                var apiResponse = await GetResponse<UserParamsResponse>(res);
                await SecureStorage.SetAsync(USERPARAMSKEYSECURESTORAGE, JsonConvert.SerializeObject(apiResponse));
                return apiResponse;
            }
            catch (Exception e)
            {
                return GetResponse<UserParamsResponse>(e);
            }
        }
    }
}
