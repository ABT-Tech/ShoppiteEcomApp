using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DellyShopApp.Views.CustomView;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VendorsFirstPage 
    {
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public object Element { get; private set; }

        public VendorsFirstPage()
        {
            InitializeComponent();

        }

        private void OrderInfoClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView stack)) return;
            switch (stack.ClassId)
            {
                case "EditProfile":
                    OpenPage(new Venderdata());
                    break;

                case "MyOder":
                    OpenPage(new CustomerReport());
                    break;

                case "MyFav":
                    OpenPage(new CustomerListPage());
                    break;

                case "LastView":
                    OpenPage(new ProductStockPage());
                    break;
            }
        }

        private void OpenPage(Page page)
        {
            Navigation.PushAsync(page)
;
        }
        protected void LogOutClick(object sender, EventArgs args)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            Xamarin.Essentials.SecureStorage.RemoveAll();
        }

        protected void LogInClick(object sender, EventArgs args)
        {
            OpenPage(new LoginPage());
        }

        
    }
}
