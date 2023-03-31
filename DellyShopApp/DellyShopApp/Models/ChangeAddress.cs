using System;using System.Collections.Generic;using System.Text;using Xamarin.Forms;

namespace DellyShopApp.Models{    public class ChangeAddress    {
        internal static object CurrentNavigation;
        private INavigation navigation;

     

        public static object Navigation { get; internal set; }
        public int AddressId { get; set; }
        public string AddressTitle { get; set; }        public string SelectCountry { get; set; }        public string SelectCity { get; set; }        public string SelectStreet { get; set; }        public string AddressDetail { get; set; }
        public string Contactnumber { get; set; }

        
    }}