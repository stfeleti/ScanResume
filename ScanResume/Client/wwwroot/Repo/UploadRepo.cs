using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace The_Resume_Reader___Hosted.Client.Interfaces
{
    class UploadRepo : IUploadRepo
    {
        readonly HttpClient _client;
        public UploadRepo(HttpClient client){
            _client = client;
        }
        public async Task<string> UploadFile(MultipartFormDataContent content)
        {
            Console.WriteLine("var postResult = await _client.PostAsync(\"api/uploadresume\", content);");
            var postResult = await _client.PostAsync("api/upload", content);
            Console.WriteLine(" Done !!");
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                Console.WriteLine(" Post Not Successfull");
                Console.WriteLine($"post content{postContent} status code{postResult.StatusCode} {postResult.Content.ReadAsStringAsync()}");
                throw new ApplicationException(postContent);
            }
            else
            {
                Console.WriteLine(" Post  Successfull");
                var fileUrl = Path.Combine("https://localhost:44367/", postContent);
                Console.WriteLine($"url {fileUrl}");
                return fileUrl;
            }
        }
    }
}