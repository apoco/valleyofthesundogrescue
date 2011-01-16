using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VOTSDR.Web.Models
{
    public class ContactUsViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        
        [Required]
        public string Comments { get; set; }
    }
}
