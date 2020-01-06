using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A4A.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Rating { get; set; }
        public int Solved { get; set; }
        public int Binding { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Type { get; set; }
    }

    public class UsersModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Rating { get; set; }
    }

    public class AddFriendModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }
    }
}