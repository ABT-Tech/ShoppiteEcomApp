using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
   public class AttributeListModel
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public int UserId { get; set; }
        public Guid ProductGUID { get; set; }
        public string[] ProductList { get; set; }
        public Boolean WishlistedProduct { get; set; }
        public int CategoryId { get; set; }
        public string SpecificationNames { get; set; }
        public int SpecificationId { get; set; }
        public string SpecificationImage { get; set; }
    }
}
