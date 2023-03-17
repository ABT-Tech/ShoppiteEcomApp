using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class ChangeAddress
    {
        public int AddressId { get; set; }
        public string AddressTitle { get; set; }
        public string SelectCountry { get; set; }
        public string SelectCity { get; set; }
        public string SelectStreet { get; set; }
        public string AddressDetail { get; set; }
    }
}
