using System;
using Newtonsoft.Json;

namespace ScanResume.Shared.Models
{
    public class Prediction
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("xmin")]
        public int XStart { get; set; }

        [JsonProperty("ymin")]
        public int YStart { get; set; }

        [JsonProperty("xmax")]
        public int XEnd { get; set; }

        [JsonProperty("ymax")]
        public int YEnd { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("ocr_text")]
        public string OcrText { get; set; }
    }
}