using System;
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
        public object Remove { get; internal set; }
        public bool VisibleItemDelete { get; internal set; }
    }
}
