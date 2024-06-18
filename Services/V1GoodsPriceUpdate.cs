using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using openapi_sdk.Utils;

namespace openapi_sdk.Services
{
    public class V1GoodsPriceUpdate {
        public class V1GoodsPriceUpdateResponse {
            [JsonProperty("requestId")]
            public string RequestId { get; set; }

            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public string Data { get; set; }
        }
        
		public class OpenDataReq {
			[JsonProperty("appid")]
			public string Appid { get; set; }

			[JsonProperty("data")]
			public string Data { get; set; }

			[JsonProperty("dataEncryptMethod")]
			public string DataEncryptMethod { get; set; }

			[JsonProperty("sign")]
			public string Sign { get; set; }

			[JsonProperty("signEncryptMethod")]
			public string SignEncryptMethod { get; set; }

			[JsonProperty("timestamp")]
			public string Timestamp { get; set; }
		}
		
	

        private HttpClient _httpClient;

        public V1GoodsPriceUpdate(string host) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            _httpClient = client;
        }

        /*V1GoodsPriceUpdate
         *Description: 开放平台更新商品价格
         * @param: body OpenDataReq OpenDataReq 必填项
         * @return: *V1GoodsPriceUpdateResponse
        */
        public V1GoodsPriceUpdateResponse? Send(string authToken, OpenDataReq body) {
            try
            {
                HttpHelper httpHelper = new HttpHelper(this._httpClient);
                string bodyStr = JsonConvert.SerializeObject(body);
                string url = this._httpClient.BaseAddress + string.Format("/v1/goodsPrice/update").TrimStart('/');
                var resp =  httpHelper.PostAsync(url, bodyStr);

                var result = resp.Result;
                return JsonConvert.DeserializeObject<V1GoodsPriceUpdateResponse>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}