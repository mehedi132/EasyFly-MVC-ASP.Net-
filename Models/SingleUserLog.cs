//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyFly.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class SingleUserLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SingleUserLog()
        {
            this.CustomerSupports = new HashSet<CustomerSupport>();
            this.Feedbacks = new HashSet<Feedback>();
            this.Memberships = new HashSet<Membership>();
            this.PassengerFlights = new HashSet<PassengerFlight>();
            this.BookedPackages = new HashSet<BookedPackage>();
        }

        [Display(Name = "User ID")]
        [DataType(DataType.Text)]
        public string S_UserID { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string S_Email { get; set; }


        [Display(Name = "Profile Photo")]
        [DataType(DataType.ImageUrl)]
        public string S_Photos { get; set; }
        public HttpPostedFileBase S_PhotoFile { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Passoword")]
        [DataType(DataType.Password)]
        public string S_Passkey { get; set; }

        [Required]
        [Display(Name = "Confirm Passoword")]
        [DataType(DataType.Password)]
        [Compare("S_Passkey", ErrorMessage = "Password didn't match!")]
        public string S_ConfirmPasskey { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Contact No.")]
        [DataType(DataType.PhoneNumber)]
        public string S_ContactNo { get; set; }

        [Required]
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        public string S_Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSupport> CustomerSupports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membership> Memberships { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassengerFlight> PassengerFlights { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookedPackage> BookedPackages { get; set; }
    }
}