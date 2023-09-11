using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DellyShopApp.Behaviors
{
    class LazyContentViewBehavior : LoadContentOnActivateBehavior<ContentView>
    {
        protected override void SetContent(ContentView element, View contentView)
        {
            element.Content = contentView;
        }
    }
}
