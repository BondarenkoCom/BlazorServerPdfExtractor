using SpecFlowPdfReader.Helpers;

namespace ExtractLibrary.Helpers
{
    public class CheckAdobe
    {
        public async Task<(string, string, string, string, string, string, string)> GetAdobeKeysValue()
        {
            try
            {
                var settingsPath = JsonHelper.GetAbodeKeys().settingPath;
                var adobeKeysID = JsonHelper.GetAbodeKeys().ClientId;
                var adobeKeysSecret = JsonHelper.GetAbodeKeys().ClientSecret;
                var adobeOrganizationId = JsonHelper.GetAbodeKeys().OrganizationId;
                var adobeAccountId = JsonHelper.GetAbodeKeys().accountId;
                var privateKeyFile = JsonHelper.GetAbodeKeys().privateKeyFile;
                var privateKeyContainText = JsonHelper.GetAbodeKeys().privateTextContain;

                return (settingsPath, adobeKeysID, adobeKeysSecret, adobeOrganizationId, adobeAccountId, privateKeyFile, privateKeyContainText);
            }
            catch (Exception ex)
            {
                return ("Settings path - error", "adobeKeysID - error", "adobeKeysSecret - error",
                    "adobeOrganizationId - error", "adobeAccountId - error", "privateKeyFile - error", ex.Message);
            }
        }
    }
}
