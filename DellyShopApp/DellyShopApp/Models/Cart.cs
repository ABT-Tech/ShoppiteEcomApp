using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public int orgId { get; set; }
        public int proId { get; set; }
        public int Qty { get; set; }
    }
}
