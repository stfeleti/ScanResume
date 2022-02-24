using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScanResume.Server.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Python.Included;
using Python.Runtime;
using System.Text;
using Mosaik.Core;
using Catalyst;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScanResume.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobFileUploadController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<string> Test()
        {
            StringBuilder output = new StringBuilder("Running python\n");
            Console.WriteLine();

            Console.WriteLine("Running python\n");
            //output.AppendLine( await SetupPython());
            //Console.WriteLine();
            //output.AppendLine(await InstallSpacy());


            Console.WriteLine("Setting python Evnironment\n");

            await Installer.SetupPython();
            PythonEngine.Initialize();
            dynamic sys = PythonEngine.ImportModule("sys");

            Console.WriteLine("Done !! Setting python Evnironment\n");
            output.AppendLine("Done !! Setting python Evnironment\n");
            output.AppendLine($"Python version:{sys.version}");

            await Installer.SetupPython();
            Installer.TryInstallPip();
            Installer.PipInstallModule("spacy==2.3.7");
            //Installer.PipInstallModule("spacy-look-data");
            PythonEngine.Initialize();
            dynamic spacy = PythonEngine.ImportModule("spacy");


            output.AppendLine("Done !! Installing Spacy");
            output.AppendLine($"Spacy version:{spacy.__version__}");

            dynamic nlp_model = spacy.load("nlp_model");
            Installer.PipInstallModule("PyMuPDF");
            dynamic fitz = PythonEngine.ImportModule("fitz");


            dynamic fname =
                @"C:\\Users\\me\\RiderProjects\\Work Projects\\Real\\CV Resume\\FixedsumeReader\\ScanResume\\ScanResume\\Server\\StaticFiles\\Resumes\\resume-sample.pdf";
            dynamic doc2 = fitz.open(fname);

            StringBuilder text = new StringBuilder();

            foreach (dynamic page in doc2)
            {
                text.Append(page.get_text());
            }

            dynamic doc = nlp_model(text.ToString());
            StringBuilder res = new StringBuilder();

            foreach (dynamic ent in doc.ents)
            {
                res.Append($"->label = {ent.label_.upper()} ->Text = {ent.text.ToString()}");
            }

            //dynamic json = PythonEngine.ImportModule("json");
           // dynamic json_result = json.dumps(res);
            return res.ToString();
        }

        private string ReadPDF(string filepath)
        {
            //string filePath = @"C:\Users\me\RiderProjects\Work Projects\Real\CV Resume\FixedsumeReader\ScanResume\ScanResume\Server\StaticFiles\Resumes\functionalSample.pdf";

            PdfReader reader = new PdfReader(filepath);
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }

            reader.Close();

            return text;
        }

        private async Task<string> SetupPython()
        {
            Console.WriteLine("Setting python Evnironment\n");
            StringBuilder output = new StringBuilder("Setting python Evnironment\n");
            await Installer.SetupPython();
            PythonEngine.Initialize();
            dynamic sys = PythonEngine.ImportModule("sys");

            Console.WriteLine("Done !! Setting python Evnironment\n");
            output.AppendLine("Done !! Setting python Evnironment\n");
            output.AppendLine($"Python version:{sys.version}");

            return output.ToString();
        }

        private async Task<string> InstallSpacy()
        {
            StringBuilder output = new StringBuilder("Installing Spacy\n");

            await Installer.SetupPython();
            Installer.TryInstallPip();
            Installer.PipInstallModule("spacy");
            Installer.PipInstallModule("spacy-look-data");
            PythonEngine.Initialize();
            dynamic spacy = PythonEngine.ImportModule("spacy");


            output.AppendLine("Done !! Installing Spacy");
            output.AppendLine($"Spacy version:{spacy.__version__}");

            return output.ToString();
        }


        // POST api/<BlobFileUploadController>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileModelDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromBody] string url)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            //Download file from Blob storage

            AzureSasCredential credential = new AzureSasCredential(
                "sp=racwdli&st=2022-02-22T14:05:44Z&se=2022-02-27T22:05:44Z&sv=2020-08-04&sr=c&sig=D2E7KI550agfvYwMgRRzBlxfwqBAFV5WEe5WRbKnRTo%3D");
            BlobClient blobClient = new BlobClient(new Uri(url), credential, new BlobClientOptions());


            string downloadFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"StaticFiles\Resumes");

            //var res = await blobClient.DownloadToAsync(downloadFilePath);

            //return Ok("Resume bois");
            Uri uri = new Uri(url);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            string filePath = $"{downloadFilePath}/{filename}";

            var result = blobClient.DownloadTo(filePath); // file is downloaded
            // check file download was success or not
            if (result.Status == 206 || result.Status == 200)
            {
                //string ress = TestSpacy(text);
                //return Ok($"{ress}");
                // You would be knowing this by now
                //return Ok(ReadPDF(filePath));
                StringBuilder output = new StringBuilder("Running python\n");
                Console.WriteLine();

                Console.WriteLine("Running python\n");
                //output.AppendLine( await SetupPython());
                //Console.WriteLine();
                //output.AppendLine(await InstallSpacy());


                Console.WriteLine("Setting python Evnironment\n");

                await Installer.SetupPython();
                PythonEngine.Initialize();
                dynamic sys = PythonEngine.ImportModule("sys");

                Console.WriteLine("Done !! Setting python Evnironment\n");
                output.AppendLine("Done !! Setting python Evnironment\n");
                output.AppendLine($"Python version:{sys.version}");

                await Installer.SetupPython();
                Installer.TryInstallPip();
                Installer.PipInstallModule("spacy==2.3.7");
                //Installer.PipInstallModule("spacy-look-data");
                PythonEngine.Initialize();
                dynamic spacy = PythonEngine.ImportModule("spacy");


                output.AppendLine("Done !! Installing Spacy");
                output.AppendLine($"Spacy version:{spacy.__version__}");

                dynamic nlp_model = spacy.load("nlp_model");
                Installer.PipInstallModule("PyMuPDF");
                dynamic fitz = PythonEngine.ImportModule("fitz");


                dynamic fname =
                    $@"C:\\Users\\me\\RiderProjects\\Work Projects\\Real\\CV Resume\\FixedsumeReader\\ScanResume\\ScanResume\\Server\\StaticFiles\\Resumes\\{filename}";
                dynamic doc2 = fitz.open(fname);

                StringBuilder text = new StringBuilder();

                foreach (dynamic page in doc2)
                {
                    text.Append(page.get_text());
                }

                dynamic doc = nlp_model(text.ToString());
                StringBuilder res = new StringBuilder();

                foreach (dynamic ent in doc.ents)
                {
                    res.Append($"{ent.label_.upper()} : {ent.text.ToString()}");
                }

                //dynamic json = PythonEngine.ImportModule("json");
                //dynamic json_result = json.dumps(res);
                return Ok(res.ToString());
            }

            return Ok($"{result.Status}");
        }
    }
}