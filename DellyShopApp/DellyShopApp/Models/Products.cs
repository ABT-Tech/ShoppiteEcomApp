using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DellyShopApp.Models
{
    public class Products
    {
        public string Status { get; set; }
        public string all { get; set; }
        public int orgId { get; set; }
        public List<ProductListModel> ProductLists { get; set; }

    }

}

