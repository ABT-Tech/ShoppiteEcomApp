using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
   public class Order
    {
        public int orgId { get; set; }
        public Guid orderGuId { get; set; }
        public int UserId { get; set; }
    }
}
