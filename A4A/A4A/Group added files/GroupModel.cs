using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A4A.Models
{
    public class GroupModel
    {
        [Display(Name = "Group ID")]
        public int GroupId { get; set; }
        [Display(Name = "Group Admin")]
        public int GroupAdmin { get; set; }
        [Display(Name = "Group Name")]
        [Required(ErrorMessage ="You must give a name for your group")]
        public string GroupName { get; set; }
    }
}