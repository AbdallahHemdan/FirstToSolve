using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A4A.Models
{
    public class GroupModel
    {
        public int GroupID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string GroupName { get; set; }
        public int AdminID { get; set; }
    }
}