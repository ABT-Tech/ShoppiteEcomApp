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
using DellyShopApp.Renderers;
using System.ComponentModel;

namespace DellyShopApp.Views.Pages.Base
{
    public class BasePage : ContentPage
    {
        private BorderlessEntry mail;
        private BorderlessEntry pass;

        public BasePage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            var safeInsets = On<iOS>().SafeAreaInsets();
            On<iOS>().SetPrefersHomeIndicatorAutoHidden(true);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True).SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            this.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(LoginPage));
            mail = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::DellyShopApp.Renderers.BorderlessEntry>(this, "email");
            pass = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::DellyShopApp.Renderers.BorderlessEntry>(this, "pswd");
        }

        internal void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
