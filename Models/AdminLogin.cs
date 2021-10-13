using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EasyFly.Models;

namespace EasyFly.Models
{
    public class AdminLogin
    {
        [Required]
        [Display(Name = "Admin ID")]
        [DataType(DataType.Text)]
        public string AdminID { get; set; }


        [Required]
        [Display(Name = "Admin Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class AdminDashboardView
    {
        
    }
}