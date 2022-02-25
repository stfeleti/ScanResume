using Newtonsoft.Json;

namespace ScanResume.Shared
{
    public class PyresparserModel
    {
        [JsonProperty("college_name")]
        public string[] CollegeName { get; set; }

        [JsonProperty("company_names")]
        public string CompanyNames { get; set; }

        [JsonProperty("degree")]
        public string[] Degree { get; set; }

        [JsonProperty("designation")]
        public string[] Designation { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("no_of_pages")]
        public long NoOfPages { get; set; }

        [JsonProperty("skills")]
        public string[] Skills { get; set; }

        [JsonProperty("total_experience")]
        public double TotalExperience { get; set; }
    }
}