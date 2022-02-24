using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage;
using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ScanResume.Shared;


namespace ScanResume.Client.Pages
{
    public partial class FileUpload : ComponentBase
    {
        private StringBuilder DropInstructions { get; set; } =
            new StringBuilder("Drag your files here or click in this area.");

        string fileUrl = string.Empty;
        private byte[] FileBytes;
        private bool FileNotDropped = true;

        private string ResumeResult;


        private FileModel FileModel { get; set; } = new();
        private async Task HandleSelected(InputFileChangeEventArgs e)
        {
            ResumeResult = null;
            //progressBar = 10.ToString("0");
            FileModel.File = e.File;
            FileNotDropped = !FileNotDropped;
            DropInstructions = new StringBuilder($"{FileModel.File.Name}");

            var buffers = new byte[ FileModel.File.Size];
            await  FileModel.File.OpenReadStream(1476485 ).ReadAsync(buffers);
            FileBytes = buffers;
            string imageType =  FileModel.File.ContentType;
            fileUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
            
        }


        private bool displayProgress = false;
        private string result = string.Empty;
        private string progressBar = 0.ToString("0");
        private int maxAllowedSize = 10 * 1024 * 1024;

        private async Task SendToBlob(IBrowserFile inputFile)
        {
            var blobUri = new Uri("https://"
                                  + "amafilewam" +
                                  ".blob.core.windows.net/" +
                                  "upload" + "/" + inputFile.Name);


            AzureSasCredential credential = new AzureSasCredential("sp=racwdli&st=2022-02-22T14:05:44Z&se=2022-02-27T22:05:44Z&sv=2020-08-04&sr=c&sig=D2E7KI550agfvYwMgRRzBlxfwqBAFV5WEe5WRbKnRTo%3D");
            BlobClient blobClient = new BlobClient(blobUri, credential, new BlobClientOptions());
            //displayProgress = true;

            var res = await blobClient.UploadAsync(inputFile.OpenReadStream(maxAllowedSize), new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders {ContentType = inputFile.ContentType},
                TransferOptions = new StorageTransferOptions
                {
                    InitialTransferSize = 1024 * 1024,
                    MaximumConcurrency = 10
                },
                ProgressHandler = new Progress<long>((progress) =>
                {
                    progressBar = (100.0 * progress / inputFile.Size).ToString("0");
                })
            });
            DropInstructions = new StringBuilder($"Successfully Uploaded {FileModel.File.Name}");
            FileModel.FileUrl = blobUri;
            //fileUrl = blobUri.AbsoluteUri;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:5001/api/BlobFileUpload"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain"); 

                    request.Content = new StringContent("\"https://amafilewam.blob.core.windows.net/upload/resume-sample.pdf\"");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json"); 

                    var response = await httpClient.SendAsync(request);

                    ResumeResult = await response.Content.ReadAsStringAsync();
                    DropInstructions = new("Done!!");
                }
            }
        }
        
        
       
        private async Task ShowWarning(string message)
        {
            await  js.InvokeVoidAsync("alert", message);
        }

        private async Task UploadFiles(EditContext arg)
        {
            if (FileNotDropped)
            {
                ShowWarning("please add File");
                return;
            }

            DropInstructions = new($"Uploading file: {FileModel.File.Name}");
            await SendToBlob( FileModel.File);
            
        }
    }
}