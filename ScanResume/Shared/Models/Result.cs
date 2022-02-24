using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScanResume.Shared.Models
{
    public class Result
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("input")]
        public Uri InputFileLocation { get; set; }

        [JsonProperty("prediction")]
        public List<Prediction> Predictions { get; set; }

        [JsonProperty("page")]
        public int PageNumber { get; set; }

        [JsonProperty("request_file_id")]
        public Guid RequestFileId { get; set; }

        [JsonProperty("filepath")]
        public string Filepath { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("rotation")]
        public int Angle { get; set; }
    }
}