using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A4A.Models
{
    public class BlogModel
    {
        public int BlogID { get; set; }
        public int BlogWriter { get; set; }
        public string BlogTitle { get; set; }
        public string BlogWriterName { get; set; }
        public string BlogContent { get; set; }
        public int GroupID { get; set; }
    }
}