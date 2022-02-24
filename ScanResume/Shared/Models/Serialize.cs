using Newtonsoft.Json;

namespace ScanResume.Shared.Models
{
    public static class Serialize
    {
        public static string ConvertToJsonString(this ResponseModel self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }
}