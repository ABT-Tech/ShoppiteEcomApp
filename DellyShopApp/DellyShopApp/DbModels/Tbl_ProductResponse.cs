using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.DbModels
{
    public class Tbl_ProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Guid ProductGUID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public int orgId { get; set; }
        public string OldPrice { get; set; }
        public Boolean WishlistedProduct { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string SpecificationNames { get; set; } = string.Empty;
        public int SpecificationId { get; set; }
        public string status { get; set; }
        public int statusid { get; set; }
        public string ProductOtherImages { get; set; }
    }
    public class Tbl_ProductMasterResponse
    {
        public List<Tbl_ProductDetailResponse> MainProductDTOs { get; set; }
    }
    public class Tbl_ProductDetailResponse
    {
        public string Status { get; set; }
        public List<Tbl_ProductResponse> productsDTOs { get; set; }
    }
}

