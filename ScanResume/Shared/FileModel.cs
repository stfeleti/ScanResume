using System;
using Microsoft.AspNetCore.Components.Forms;

namespace ScanResume.Shared
{
    public class FileModel
    {
        public IBrowserFile File { get; set; }
        public Uri FileUrl { get; set; }

        
    }
}