using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using DellyShopApp.Models;
using DellyShopApp.Services;
using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage 
	{
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);

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
            var registration = new Registration
            {
                Username = username.Text,
                Email = EmailAddress.Text,
                Password = Pswd.Text,
                ConfirmPassword = cnfm.Text,
                ContactNumber = phonenumber.Text,
                Address = address.Text,
                State = statename.Text,
                city = cityname.Text,
                Zipcode = zipcode.Text,
                OrgId = orgId,

            };
            await Navigation.PushAsync(new LoginPage());
            await DataService.Registration(registration);
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool EmailCheck = regex.IsMatch(EmailAddress.Text.Trim());

            if (!EmailCheck)
            {
                await DisplayAlert("opps..", "Invalid Email Address", "Ok");
                return;
            }
            else if (Pswd.Text != cnfm.Text)
            {
               await DisplayAlert("opps..", "Confirm Password Not Match", "Ok");
                return;
            }

            else
            {
                await DisplayAlert("Congrulations", "You are Registered", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }
           
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
    
}
