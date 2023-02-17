using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.ModalPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewAddressPage
    {
        public AddNewAddressPage()
        {
            InitializeComponent();
        }

        private void ClosePageClick(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}