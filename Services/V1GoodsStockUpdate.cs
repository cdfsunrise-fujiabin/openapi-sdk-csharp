using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using openapi_sdk.Utils;


namespace openapi_sdk.Services
{
    public class V1GoodsStockUpdate {
        public class V1GoodsStockUpdateResponse {
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

        public V1GoodsStockUpdate(string host) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            _httpClient = client;
        }

        /*V1GoodsStockUpdate
         *Description: 开放平台更新商品库存
         * @param: body OpenDataReq OpenDataReq 必填项
         * @return: *V1GoodsStockUpdateResponse
        */
        public V1GoodsStockUpdateResponse? Send(string authToken, OpenDataReq body) {
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", authToken }
                };
                HttpHelper httpHelper = new HttpHelper(this._httpClient, headers);
                string bodyStr = JsonConvert.SerializeObject(body);
                string url = this._httpClient.BaseAddress + string.Format("/v1/goodsStock/update").TrimStart('/');
                var resp =  httpHelper.PostAsync(url, bodyStr);

                var result = resp.Result;
                return JsonConvert.DeserializeObject<V1GoodsStockUpdateResponse>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}