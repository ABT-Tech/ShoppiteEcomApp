using System;
using System.Collections.Generic;

namespace DellyShopApp.Models
{
    public class Login
    {
        public int org_Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string type { get; set; }
    }
}
