using System;
using Newtonsoft.Json;

namespace ScanResume.Shared.Models
{
    public partial class UploadFile
    {
        [JsonProperty("original")]
        public Uri Original { get; set; }

        [JsonProperty("original_compressed")]
        public Uri OriginalCompressed { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("acw_rotate_90")]
        public Uri AcwRotate90 { get; set; }

        [JsonProperty("acw_rotate_180")]
        public Uri AcwRotate180 { get; set; }

        [JsonProperty("acw_rotate_270")]
        public Uri AcwRotate270 { get; set; }

        [JsonProperty("original_with_long_expiry")]
        public Uri OriginalWithLongExpiry { get; set; }
    }
}