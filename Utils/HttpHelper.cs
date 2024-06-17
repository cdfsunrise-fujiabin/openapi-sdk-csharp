using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace openapi_sdk.Utils
{
    public class HttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpHelper(HttpClient httpClient, Dictionary<string, string>? headers)
        {
            _httpClient = httpClient;
            if (headers != null)
            {
                foreach (var (headerName, headerValue) in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
                }
            }
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string body)
        {
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}