using System;

using Xamarin.Forms;

namespace DellyShopApp.Models
{
    public class Attributes
    {
        public string SpecificationNames { get; set; }
        public int SpecificationIds { get; set; }
        public bool IsSpecificationExist { get; set; }
        public int DefaultSpecification { get; set; }
        public Guid ProductGUId { get; set; }
        public int OrgId { get; set; }
        public int UserId { get; set; }
        public Color BGColor { get; set; }
    }
}

