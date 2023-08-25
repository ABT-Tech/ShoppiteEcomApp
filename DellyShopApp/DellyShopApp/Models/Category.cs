using System;

namespace DellyShopApp.Models
{
    public class Category
    {
       

        public string Banner { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public int orgID { get; set; }
        public string SpecificationNames { get; set; }
        public int SpecificationIds { get; set; }
        public bool IsCouponEnabled { get; set; }


    }
}
