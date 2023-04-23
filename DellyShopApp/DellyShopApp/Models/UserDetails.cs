using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class UserDetails
    {
        public List<OrderListModel> ProductLists { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public object TotalPrice { get; set; }
        public int orgId { get; set; }

    }
}