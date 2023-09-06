using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellyShopApp.Models
{
    public class ProductResponse
    {
        public string Title { get; set; }
        public Guid ProductGUID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public string[] ProductList { get; set; }
        public int OldPrice { get; set; }
        public Boolean WishlistedProduct { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string SpecificationNames { get; set; }
        public int SpecificationId { get; set; }
    }
    public class ProductMasterResponse
    {
        public List<ProductDetailResponse> MainProductDTOs { get; set; }
    }
    public class ProductDetailResponse
    {
        public string Status { get; set; }
        public List<ProductResponse> productsDTOs { get; set; }
    }
}
