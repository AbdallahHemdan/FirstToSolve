using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace A4A.Models
{
    public class SubmissionModel
    {
        public int SubmissionID { get; set; }
        public int ContestantID { get; set; }
        public string SubmissionVerdict { get; set; }
        public int SubmissionMemory { get; set; }
        public int SubmissionTime { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string SubmissionLang { get; set; }
        public string ProblemID { get; set; }
    }
}