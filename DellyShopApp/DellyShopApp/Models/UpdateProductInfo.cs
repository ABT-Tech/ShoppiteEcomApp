using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
   public class UpdateProductInfo
    {
        public double Price { get; set; }
        public int orgId { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
