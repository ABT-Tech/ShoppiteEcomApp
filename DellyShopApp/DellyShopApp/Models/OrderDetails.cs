using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
   public class OrderDetails
    {
        public List<OrderListModel> ProductLists { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public object TotalPrice { get; set; }
        public int orgId { get; set; }
        public int userId { get; set; }
        public int orderId { get; set; }
    }
}
