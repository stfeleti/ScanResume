using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ScanResume.Client.Service_Interfaces;

namespace ScanResume.Client.Services
{
    public class FileHttpRepository : IFileHttpRepository
    {
        private readonly HttpClient _client;
        public FileHttpRepository(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public async Task<string> UploadFile(MultipartFormDataContent content, string fileUrl)
        {
            var postResult = await _client.PostAsync("/api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine(fileUrl, postContent);
                return imgUrl;
            }
        }
    }
}
