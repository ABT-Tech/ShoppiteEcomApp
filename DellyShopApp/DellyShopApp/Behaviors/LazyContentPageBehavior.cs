using DellyShopApp.Views.Pages.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DellyShopApp.Behaviors
{
    class LazyContentPageBehavior : LoadContentOnActivateBehavior<BasePage>
    {
        protected override void SetContent(BasePage page, View contentView)
        {
            page.Content = contentView;
        }
    }
}
