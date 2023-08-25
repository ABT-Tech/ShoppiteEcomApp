using System;using System.Collections.Generic;using System.Text;using Xamarin.Forms;

namespace DellyShopApp.Models{    public class ChangeAddress    {
        internal static object CurrentNavigation;
        private INavigation navigation;

        public int AddressId { get; set; }
        public string SelectCity { get; set; }
        public string SelectState { get; set; }
        public string AddressDetail { get; set; }
        public string zipcode { get; set; }
        public string Contactnumber { get; set; }

    }}