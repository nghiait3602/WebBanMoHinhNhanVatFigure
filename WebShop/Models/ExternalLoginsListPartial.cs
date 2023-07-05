using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebShop.Models
{
    public class ExternalLoginsListPartial
    {
           public string ReturnUrl { get; set; }
    }
    public class ExternalLoginsConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}