using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DellyShopApp.Views.Pages
{	
	public partial class imagepopup: Popup
	{	
		public imagepopup (string image)
		{
			InitializeComponent ();
			showimg.Source = image;
        }
    }
}

