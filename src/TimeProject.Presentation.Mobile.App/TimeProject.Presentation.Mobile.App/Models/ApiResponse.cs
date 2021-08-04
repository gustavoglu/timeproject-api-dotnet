using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Transactions;

namespace TimeProject.Presentation.Mobile.App.Models
{
    public class ApiResponse<T>
    {

      
        public ApiResponse(bool success, T data, List<KeyValuePair<string, string>> errors = null)
        {
            Success = success;
            Errors = errors ?? new List<KeyValuePair<string, string>>();
            Data = data;
        }

        public bool Success { get; set; }
        public List<KeyValuePair<string, string>> Errors { get; set; }
        public T Data { get; set; }
    }
}
