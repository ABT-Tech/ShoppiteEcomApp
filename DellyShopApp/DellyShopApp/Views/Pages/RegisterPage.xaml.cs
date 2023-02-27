using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace DellyShopApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage 
	{
		public RegisterPage ()
		{
		   

            InitializeComponent ();
		  
		}
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	     
        }
        private async void RegisteruButtonClick(object sender, EventArgs e)
	    {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool EmailCheck = regex.IsMatch(EmailAddress.Text.Trim());

            if (!EmailCheck)
            {
                DisplayAlert("opps..", "Invalid Email Address", "Ok");
                return;
            }
            else if (Pswd.Text != cnfm.Text)
            {
                DisplayAlert("opps..", "Confirm Password Not Match", "Ok");
                return;
            }

            else
            {
                DisplayAlert("Congrulations", "You are Registered", "Ok");
                Navigation.PushAsync(new LoginPage());
            }

        }

		private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
    
}
