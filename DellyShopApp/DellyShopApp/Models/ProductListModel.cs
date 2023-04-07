using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DellyShopApp.ViewModel;

namespace DellyShopApp.Models
{
    public class ProductListModel : BaseVm
    {
        private bool _visibleDeleteItem;
        public bool VisibleItemDelete
        {
            get => _visibleDeleteItem;
            set
            {
                _visibleDeleteItem = value;
                OnPropertyChanged(nameof(VisibleItemDelete));
            }
        }


        private int _rotate;
        internal string orderStatus;
       
        private int _Quantity;
        internal double product;

        public int Rotate
        {
            get => _rotate;
            set
            {
                _rotate = value;
                OnPropertyChanged(nameof(Rotate));
            }
        }




        public string Title { get; set; }
        public string Brand { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public string[] ProductList { get; set; }
        public int OldPrice { get; set; }
        public int orderId { get; set; }
        public Guid OrderGuId { get;  set; }
        public DateTime InsertDate { get;  set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public ChangeAddress changeAddress { get; set; }
        public OrderCheckOut ordercheckout { get; set; }
        public object BaseTotalPrice  { get;  set; }
     




        public object Children { get;  set; }
        public object RemoveCommand { get;  set; }
        public int Empty { get; internal set; }
        public object Name { get; internal set; }

      
       
    }
}

