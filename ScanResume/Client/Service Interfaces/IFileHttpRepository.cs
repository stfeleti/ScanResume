using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScanResume.Client.Service_Interfaces
{
    public interface IFileHttpRepository
    {
        Task<string> UploadFile(MultipartFormDataContent content, string fileUrl);
    }
}
