using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class OrderCheckOut
    {
        public int orgid { get; set; }
       
        public List<ProductListModel> ProductLists { get; set; }
        public ChangeAddress Address { get; set; }
        public object BaseTotalPrice { get; set; }
        public object TotalPrice { get; set; }

        internal List<OrderCheckOut> ToList()
        {
            throw new NotImplementedException();
        }
    }
}

