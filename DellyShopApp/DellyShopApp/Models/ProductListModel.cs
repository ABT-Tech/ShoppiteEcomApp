using DellyShopApp.ViewModel;
using System;
using System.Drawing;

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
        internal Guid orderGuid;
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
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int orgId { get; set; }
        public string[] ProductList { get; set; }
        public int OldPrice { get; set; }
        public int orderId { get; set; }
        public Guid OrderGuId { get;  set; }
        public DateTime InsertDate { get;  set; }
        public DateTime orderDate { get;  set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public ChangeAddress changeAddress { get; set; }
        public OrderCheckOut ordercheckout { get; set; }
        public object TotalPrice  { get;  set; }
        public object Children { get;  set; }
        public object RemoveCommand { get;  set; }
        public int Empty { get;  set; }
        public int OrderNumber { get;  set; }
        public int productQty { get;  set; }
        public string Discription { get; set; }
        public string orderStatus { get; set; }
        public bool IsPriceVisible { get; set; }
        public bool IsOutStock { get; set; }
        public bool WishlistedProduct { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Attribute{ get; set; }
        public int AttributeId{ get; set; }
        public Guid ProductGUId { get; set; }
        public int SpecificationIds { get; set; }
        public string SpecificationNames { get; set; }
        public bool ISSpecificationNames { get; set; }
        public Color BGColor { get; set; }
        public string SelectedSpecification { get; set; }
        public int DefaultSpecification { get; set; }
    }
}

