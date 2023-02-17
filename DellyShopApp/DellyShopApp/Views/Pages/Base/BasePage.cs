using System;
using DellyShopApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DellyShopApp.Languages;
using Xamarin.Forms;
using DellyShopApp.Helpers;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DellyShopApp.Views.Pages.Base
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
             Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            var safeInsets = On<iOS>().SafeAreaInsets();
            On<iOS>().SetPrefersHomeIndicatorAutoHidden(true);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True).SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            this.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
    }
}