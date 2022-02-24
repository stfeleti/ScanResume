using System;
using Microsoft.AspNetCore.Components.Forms;

namespace ScanResume.Server.Models
{
    public class FileModelDto
    {
        public byte[] File { get; set; }
        public string FileUrl { get; set; }
    }
}