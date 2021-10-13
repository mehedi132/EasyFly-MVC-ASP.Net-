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

    public partial class HotelInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelInfo()
        {
            this.PassengerFlights = new HashSet<PassengerFlight>();
        }


        [Required]
        [Display(Name = "Hotel ID")]
        [DataType(DataType.Text)]
        public string HotelID { get; set; }


        [Required]
        [Display(Name = "Hotel Name")]
        [DataType(DataType.Text)]
        public string HotelName { get; set; }


        [Required]
        [Display(Name = "Hotel Address")]
        [DataType(DataType.Text)]
        public string HotelAddress { get; set; }


        [Required]
        [Display(Name = "Hotel Email")]
        [DataType(DataType.EmailAddress)]
        public string HotelMail { get; set; }


        [Display(Name = "Hotel Photo")]
        [DataType(DataType.ImageUrl)]
        public string HotelPhotos { get; set; }
        public HttpPostedFileBase H_PhotoFile { get; set; }


        [Required]
        [Display(Name = "Business Rooms")]
        [DataType(DataType.Text)]
        public Nullable<int> BusinessCapacity { get; set; }


        [Required]
        [Display(Name = "Economy Rooms")]
        [DataType(DataType.Text)]
        public Nullable<int> EconomyCapacity { get; set; }


        [Required]
        [Display(Name = "Economy Rent")]
        [DataType(DataType.Currency)]
        public int EcoRent { get; set; }


        [Required]
        [Display(Name = "Business Rent")]
        [DataType(DataType.Currency)]
        public int BusiRent { get; set; }


        [Required]
        [Display(Name = "Contact No.")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNo { get; set; }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassengerFlight> PassengerFlights { get; set; }
    }
}