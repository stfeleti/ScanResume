using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScanResume.Shared.Models
{
    public class ResponseModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<Result> Result { get; set; }

        [JsonProperty("signed_urls")]
        public SignedUrls SignedUrls { get; set; }
        
        public static ResponseModel ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<ResponseModel>(json, Converter.Settings);
        }
    }
    
}