using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using openapi_sdk.Utils;


namespace openapi_sdk.Services
{
    public class V2UserAuth {
        public class V2UserAuthResponse {
            [JsonProperty("requestId")]
            public string RequestId { get; set; }

            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public string Data { get; set; }
        }
        
		public class OpenAuthReq {
			[JsonProperty("appid")]
			public string Appid { get; set; }

			[JsonProperty("password")]
			public string Password { get; set; }
		}
		
	

        private HttpClient _httpClient;

        public V2UserAuth(string host) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            _httpClient = client;
        }

        /*V2UserAuth
         *Description: 
         * @param: body OpenAuthReq OpenAuthReq 必填项
         * @return: *V2UserAuthResponse
        */
        public V2UserAuthResponse? Send(OpenAuthReq body) {
            try
            {
                HttpHelper httpHelper = new HttpHelper(this._httpClient);
                string bodyStr = JsonConvert.SerializeObject(body);
                string url = this._httpClient.BaseAddress + string.Format("/v2/user/auth").TrimStart('/');
                var resp =  httpHelper.PostAsync(url, bodyStr);

                var result = resp.Result;
                return JsonConvert.DeserializeObject<V2UserAuthResponse>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}