using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using openapi_sdk.Utils;

namespace openapi_sdk.Services
{
    public class V1QueryGoodsCategory {
        public class V1QueryGoodsCategoryResponse {
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

        public V1QueryGoodsCategory(string host) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            _httpClient = client;
        }

        /*V1QueryGoodsCategory
         *Description: 开放平台商品分类查询
         * @param: body OpenDataReq OpenDataReq 必填项
         * @return: *V1QueryGoodsCategoryResponse
        */
        public V1QueryGoodsCategoryResponse? Send(string authToken, OpenDataReq body) {
            try
            {
                HttpHelper httpHelper = new HttpHelper(this._httpClient);
                string bodyStr = JsonConvert.SerializeObject(body);
                string url = this._httpClient.BaseAddress + string.Format("/v1/query/goodsCategory").TrimStart('/');
                var resp =  httpHelper.PostAsync(url, bodyStr);

                var result = resp.Result;
                return JsonConvert.DeserializeObject<V1QueryGoodsCategoryResponse>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}