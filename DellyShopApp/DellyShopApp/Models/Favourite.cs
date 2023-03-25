using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.Models
{
    public class Favourite
    {
        public int UserId { get; set; }
        public int orgId { get; set; }
        public int proId { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
    }
}
