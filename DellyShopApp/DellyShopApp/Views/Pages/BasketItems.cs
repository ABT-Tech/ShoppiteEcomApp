using System.Collections.ObjectModel;
using DellyShopApp.Models;

namespace DellyShopApp.Views.Pages
{
    internal class BasketItems
    {
        public static ObservableCollection<Order> ItemsSource { get; internal set; }
    }
}