using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using openapi_sdk.Utils;

public class OpenAuthReq
{
    [JsonProperty("appid")]
    public string AppId { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
}

public class V2UserAuth
{
    private HttpClient _httpClient;
    
    public V2UserAuth(string host) {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(host); 
        _httpClient = client;
    }

    public string GetToken(OpenAuthReq req)
    {
        try
        {
            HttpHelper httpHelper = new HttpHelper(this._httpClient);
            string body = JsonConvert.SerializeObject(req);
            string url = this._httpClient.BaseAddress + "/v2/user/auth".TrimStart('/');
            var resp =  httpHelper.PostAsync(url, body);

            var result = resp.Result;
            Console.WriteLine(result);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}