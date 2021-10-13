using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyFly.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }


        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Passkey { get; set; }
    }

}