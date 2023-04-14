using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DellyShopApp.ViewModel;

namespace DellyShopApp.Models
{
    public class OrderListModel : BaseVm
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public int orderId { get; set; }
        public Guid OrderGuId { get;  set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }      
    }
}

