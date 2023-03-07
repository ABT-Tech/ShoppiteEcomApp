using DellyShopApp.ViewModel;using System;using System.Collections.Generic;using System.Text;namespace DellyShopApp.Models{
    public class MyCartListModel : BaseVm    {        private bool _visibleDeleteItem;        public bool VisibleItemDelete        {
            get => _visibleDeleteItem;
            set
            {
                _visibleDeleteItem = value;
                OnPropertyChanged(nameof(VisibleItemDelete));
            }        }

        private int _rotate;        public int Rotate        {            get => _rotate;            set            {                _rotate = value;                OnPropertyChanged(nameof(Rotate));            }        }        public string[] ProductList { get; set; }



    }}