using System;
using System.Collections.Generic;

namespace ScanResume.Shared
{
    public class AIResult
    {
        public List<College> Colleges { get; set; }
        public List<Company> Companies { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Unknown { get; set; }
        public string YearsOfExperince { get; set; }
    }

    public class Company
    {
        public string Designation { get; set; }
    }

    public class College
    {
        public string CollegeName { get; set; }
        public string Degree { get; set; }
        public DateTime GraduationYear { get; set; }
        
    }
}