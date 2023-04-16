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

        public async Task<DateTime> GetExpirationDateAsync()
        {
            (string accessToken, DateTime expirationDate) = await GetAccessTokenAsync(ClientId, ClientSecret);
            return expirationDate;
        }

        private static async Task<(string, DateTime)> GetAccessTokenAsync(string clientId, string clientSecret)
        {
            string tokenUrl = "https://ims-na1.adobelogin.com/ims/token/v1";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "https://ims-na1.adobelogin.com/s/ent_documentcloud_sdk")
            });

            HttpResponseMessage response = await httpClient.PostAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(jsonResponse);
            string accessToken = json["access_token"].ToString();

            int expiresIn = json.Value<int>("expires_in");
            DateTime expirationDate = DateTime.UtcNow.AddSeconds(expiresIn);

            return (accessToken, expirationDate);
        }
    }
}
