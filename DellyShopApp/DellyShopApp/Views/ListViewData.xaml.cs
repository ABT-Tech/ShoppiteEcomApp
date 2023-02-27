using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewData : ContentPage
    {
        public IList<shop> Shops { get; set; }
        public ListViewData()
        {
            InitializeComponent();
            Shops = new List<shop>();
            Shops.Add(new shop 
            {
                Name="Grocery Shore",
                OrgID=1,
                Image="Grocery_Store"
            });
        }
        public class shop
        {
            public string Name { get; set; }
            public int OrgID { get; set; }
            public string Image { get; set; }
        }
    }
}