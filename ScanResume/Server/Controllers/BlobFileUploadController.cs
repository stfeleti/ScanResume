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
            output.AppendLine( await SetupPython());
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

            return res.ToString();
            return $"{text.ToString()}";
            
            // using (Py.GIL())
            // {
            //     dynamic os = Py.Import("os");
            //     dynamic dir = os.listdir();
            //     Console.WriteLine(dir);
            //
            //     foreach (var d in dir)
            //     {
            //         return d + "PY.CL";
            //     }
            // }
            //
            // return output.ToString();
            // try
            // {
            //     using var @lock = await spacy.Initialize(Spacy.ModelSize.Small, Language.Any, Language.English);
            // }
            // catch (Exception e)
            // {
            //      
            // }
            //
            //
            // var nlp = Spacy.For(Spacy.ModelSize.Small, Language.English);
            //
            // var doc = new Document("This is a test of integrating Spacy and Catalyst", Language.English);
            //
            // nlp.ProcessSingle(doc);
            //
            // Console.WriteLine(doc.ToJson());
            // return doc.ToJson();
        }
        private string GetResults()
        {
           
                // 1) Create Process Info
                var psi = new ProcessStartInfo();
                psi.FileName = @"C:\Program Files\Python39\python.exe";
                // 2) Provide script and arguments
                var script = @"C:\Users\Kian\PycharmProjects\ResumeParser\main.py";
                psi.Arguments = $"\"{script}\" ";
                // 3) Process configuration
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                // 4) Execute process and get output
                var errors = "";
                var output = "";
                Console.WriteLine("starting script"); 
                var process = Process.Start(psi);
                errors = process.StandardError.ReadToEnd();
                output = process.StandardOutput.ReadToEnd();
                // 5) Display output
                Console.WriteLine("ERRORS:");
                Console.WriteLine(errors);
                Console.WriteLine();
                Console.WriteLine("Results:");
                Console.WriteLine(output);
            
            return output; 
            
        }
        // [HttpGet]
        //public async Task<IActionResult> RunPython()
        //{
        //    StringBuilder output = new StringBuilder("Running python\n");
        //    Console.WriteLine();
            
        //    Console.WriteLine("Running python\n");
        //    output.AppendLine( await SetupPython());
        //    //Console.WriteLine();
        //    //output.AppendLine(await InstallSpacy());
            


        //    output.Append(ReadPDF());
        
        //    return Ok($"output {output.ToString()}");
        // public async Task<IActionResult> RunPython()
        // {
        //     StringBuilder output = new StringBuilder("Running python\n");
        //     Console.WriteLine();
        //     
        //     Console.WriteLine("Running python\n");
        //     //output.AppendLine( await SetupPython());
        //     //Console.WriteLine();
        //     //output.AppendLine(await InstallSpacy());
        //     
        //
        //
        //     output.Append(ReadPDF());
        //
        //     return Ok($"output {output.ToString()}");
        // }

        
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
            
            AzureSasCredential credential = new AzureSasCredential("sp=racwdli&st=2022-02-22T14:05:44Z&se=2022-02-27T22:05:44Z&sv=2020-08-04&sr=c&sig=D2E7KI550agfvYwMgRRzBlxfwqBAFV5WEe5WRbKnRTo%3D");
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
                return Ok(ReadPDF(filePath));
                using (StreamReader r = new StreamReader(filePath))
                {
                    string content = r.ReadToEnd();
                    return Ok($"{content}");// I don't know what you want to do with this
                }
            }
            return Ok($"{result.Status}");
        }

        private string TestSpacy(string text)
        {
            //var spacy = new Spacy();

            //var nlp = spacy.Load("en_core_web_sm");
            //var doc = nlp.GetDocument("Apple is looking at buying U.K. startup for $1 billion");

            //foreach (Token token in doc.Tokens)
            //    Console.WriteLine($"{token.Text} {token.Lemma} {token.PoS} {token.Tag} {token.Dep} {token.Shape} {token.IsAlpha} {token.IsStop}");

            //Console.WriteLine("");
            //foreach (Span ent in doc.Ents)
            //    Console.WriteLine($"{ent.Text} {ent.StartChar} {ent.EndChar} {ent.Label}");

            //nlp = spacy.Load("en_core_web_md");
            //var tokens = nlp.GetDocument("dog cat banana afskfsd");

            //Console.WriteLine("");
            //foreach (Token token in tokens.Tokens)
            //    Console.WriteLine($"{token.Text} {token.HasVector} {token.VectorNorm}, {token.IsOov}");

            //tokens = nlp.GetDocument("dog cat banana");
            //Console.WriteLine("");
            //foreach (Token token1 in tokens.Tokens)
            //{
            //    foreach (Token token2 in tokens.Tokens)
            //        Console.WriteLine($"{token1.Text} {token2.Text} {token1.Similarity(token2) }");
            //}

            //doc = nlp.GetDocument("I love coffee");
            //Console.WriteLine("");
            //Console.WriteLine(doc.Vocab.Strings["coffee"]);
            //Console.WriteLine(doc.Vocab.Strings[3197928453018144401]);

            //Console.WriteLine("");
            //foreach (Token word in doc.Tokens)
            //{
            //    var lexeme = doc.Vocab[word.Text];
            //    Console.WriteLine($@"{lexeme.Text} {lexeme.Orth} {lexeme.Shape} {lexeme.Prefix} {lexeme.Suffix} {lexeme.IsAlpha} {lexeme.IsDigit} {lexeme.IsTitle} {lexeme.Lang}");
            //}
            return "";
        }
    }
}
