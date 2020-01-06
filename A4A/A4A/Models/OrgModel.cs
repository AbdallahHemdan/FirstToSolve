using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A4A.Models
{
    public class OrgModel
    {
        public int OrgID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string OrgName { get; set; }
        public int AdminID { get; set; }
    }
}