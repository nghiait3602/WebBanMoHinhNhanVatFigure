using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class CustomerNameViewModel
    {
        public string CustomerName { get; set;}
        public string Phone { get; set;}
        public string Address { get; set;}
        public string Email { get; set;}
        public int TypePayment { get; set; }
    }
}