using Newtonsoft.Json;

namespace ExtractLibrary.Models
{
    public class FontInfo
    {
        [JsonProperty("font_type")]
        public string FontType { get; set; }
    }
}
