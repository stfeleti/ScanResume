using System.Net.Http;
using System.Threading.Tasks;

namespace The_Resume_Reader___Hosted.Client.Interfaces
{
    public interface IUploadRepo
    {
        Task<string> UploadFile(MultipartFormDataContent content);
    }
}