using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A4A.Models
{
    public class TeamModel
    {
        public int TeamID { get; set; }

        [Required (ErrorMessage = "This Field is required")]
        public string TeamName { get; set; }
        public int Rating { get; set; }
        public int LeaderID { get; set; }

        [Required (ErrorMessage = "This Field is required")]
        public string Member2 { get; set; }

        [Required (ErrorMessage = "This Field is required")]
        public string Member3 { get; set; }
    }
}