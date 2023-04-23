using System;
using System.Collections.Generic;

namespace DellyShopApp.Models
{
    public class ForgetPassword
    {
        public int OrgId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
