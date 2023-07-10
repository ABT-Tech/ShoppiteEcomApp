using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DellyShopApp.Models
{
    public class Orders
    {
        public int orgId { get; set; }
        public string Remark { get; set; }
        public string orderstatus { get; set; }
        public int orderId { get; set; }
        public int UserId { get; set; }
    }
}

