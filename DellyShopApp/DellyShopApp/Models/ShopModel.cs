using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class ShopModel
    {
        public string ShopName { get; set; }
        public int  OrgId { get; set; }
        public string Image { get; set; }
       

        public static implicit operator int(ShopModel v)
        {
            throw new NotImplementedException();
        }
    }
}
