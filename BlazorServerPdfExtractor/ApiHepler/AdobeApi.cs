using Newtonsoft.Json.Linq;
using SpecFlowPdfReader.Helpers;

namespace BlazorServerPdfExtractor.ApiHelper
{
    public class AdobeApi
    {
        public string SettingsPath = JsonHelper.GetAbodeKeys().settingPath;
        public string ClientId = JsonHelper.GetAbodeKeys().ClientId;
        public string ClientSecret = JsonHelper.GetAbodeKeys().ClientSecret;
        public string OrganizationId = JsonHelper.GetAbodeKeys().OrganizationId;
        public string AccountId = JsonHelper.GetAbodeKeys().accountId;
        public string PrivateKeyFile = JsonHelper.GetAbodeKeys().privateKeyFile;
        public string PrivateKeyContainText = JsonHelper.GetAbodeKeys().privateTextContain;

        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<string> GetAccess()
        {

            var accessToken = await GetAccessTokenAsync(ClientId, ClientSecret);
            return accessToken;
        }

        public static async Task<string> GetAccessTokenAsync(string clientId, string clientSecret)
        {
            string tokenUrl = "https://ims-na1.adobelogin.com/ims/token";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var response = await httpClient.PostAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(jsonResponse);
            Console.WriteLine(json["access_token"].ToString());
            return json["access_token"].ToString();
        }
    }
}