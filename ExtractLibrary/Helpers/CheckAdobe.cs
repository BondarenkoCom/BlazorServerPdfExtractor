using SpecFlowPdfReader.Helpers;

namespace ExtractLibrary.Helpers
{
    public class CheckAdobe
    {
        public async Task<(string, string)> GetAdobeKeysValue()
        {
            try
            {
                var adobeKeysID = JsonHelper.GetAbodeKeys().ClientId;
                var adobeKeysSecret = JsonHelper.GetAbodeKeys().ClientSecret;

                return (adobeKeysID, adobeKeysSecret);

            }
            catch (Exception ex)
            {
                return ("error", ex.Message);
            }
        }
    }
}
