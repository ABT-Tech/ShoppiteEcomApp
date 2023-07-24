using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class UserOrder
    {
        public int orgId { get; set; }
        public int userId { get; set; }
        public int orderId { get; set; }
        public int SpecificationIds { get; set; }
        public string SpecificationNames { get; set; }
        public double Price { get; set; }
    }
}