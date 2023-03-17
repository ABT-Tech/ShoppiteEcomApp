using System.Collections.Generic;
using DellyShopApp.Models;
using Xamarin.Forms;

namespace DellyShopApp.Renderers
{
    public class BorderlessEntry : Entry
    {
        public List<ChangeAddress> ItemsSource { get; internal set; }
    }
}
