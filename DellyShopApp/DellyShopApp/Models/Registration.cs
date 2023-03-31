using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
   public class Registration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string city { get; set; }
        public string Address { get; set; }
        public int OrgId { get; set; }
    }
}
