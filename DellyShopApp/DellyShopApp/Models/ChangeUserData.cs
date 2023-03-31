using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DellyShopApp.Models;

namespace DellyShopApp.Views.Pages
{
    public class ChangeUserData
    {
        public static ObservableCollection<ProductListModel> ItemsSource { get; internal set; }
        public int UserId { get; set; }
        public  string ChangeName { get; set; }
        public  string ChangeEmail { get; set; }
        public  string ChangePhoneNumber { get; set; }
        public  string ChangeAddress { get; set; }
        public string ChangeState { get; set; }
        public string ChangeCity { get; set; }
        public string ChangeZipCode { get; set; }
        public string ChangeSteet { get; set; }
        public string Contactnumber { get; set; }



    }   
}
 