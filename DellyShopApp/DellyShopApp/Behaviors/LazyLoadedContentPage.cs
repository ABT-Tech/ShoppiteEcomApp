using DellyShopApp.DependencyServices;
using DellyShopApp.Views.Pages.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DellyShopApp.Behaviors
{
    class LazyLoadedContentPage : BasePage, IActiveAware
    {
        public event EventHandler IsActiveChanged;

        bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
