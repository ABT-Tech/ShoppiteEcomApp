using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using DellyShopApp.Models;
using Xamarin.Essentials;
using DellyShopApp.Services;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace DellyShopApp.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage 
	{
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public RegisterPage ()
		{

            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            InitializeComponent();
		  
		}
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	     
        }
        private async void RegisteruButtonClick(object sender, EventArgs e)
	    {

            var registration = new Registration
            {
                Username = UserName.Text,
                Email = EmailAddress.Text,
                Password = Pswd.Text,
                ConfirmPassword = cnfm.Text,
                Address = address.Text,
                State = statename.Text,
                city = cityname.Text,
                Zipcode = zipcode.Text,
                ContactNumber = number.Text,
                OrgId = orgId,
                

            };
            if (UserName.Text == null || UserName.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your UserName", "Ok");
                return;
            }
            else if (EmailAddress.Text == null || EmailAddress.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your EmailAddress", "Ok");
                return;

            }
            else if (Pswd.Text == null || Pswd.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your Password", "Ok");
                return;
            }
            else if (cnfm.Text == null || cnfm.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your Conform Password", "Ok");
                return;
            }
            else if (number.Text == null || number.Text == "" || number.Text.Length<10)
            {
                await DisplayAlert("opps..", "Please Enter Your Phonenumber", "Ok");
                return;
            }
            else if (address.Text == null || address.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your Address", "Ok");
                return;
            }
            else if (statename.Text == null || statename.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your State Name", "Ok");
                return;
            }
            else if (cityname.Text == null || cityname.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your City", "Ok");
                return;
            }
            else if (zipcode.Text == null || zipcode.Text == "" || number.Text.Length<6)
            {
                await DisplayAlert("opps..", "Please Enter Your ZipCode", "Ok");
                return;
            }


            //await Navigation.PushAsync(new LoginPage());
           var reg = await DataService.Registration(registration);



            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool EmailCheck = regex.IsMatch(EmailAddress.Text.Trim());

            if (UserName.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your UserName", "Ok");
                return;
            }
            else if (EmailAddress.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your EmailAddress", "Ok");
                return;

            }
            else if (Pswd.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your Password", "Ok");
                return;
            }
            else if (cnfm.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your Conform Password", "Ok");
                return;
            }
            else if (number.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your Phonenumber", "Ok");
                return;
            }
            else if (address.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your Address", "Ok");
                return;
            }
            else if (statename.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your State Name", "Ok");
                return;
            }
            else if (cityname.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your City", "Ok");
                return;
            }
            else if (zipcode.Text == null)
            {
                await DisplayAlert("opps..", "Please Enter Your ZipCode", "Ok");
                return;
            }
            else if (!EmailCheck)
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
                if (reg == "You are Registered!!")
                {
                    await DisplayAlert("Congrulations", reg, "Ok");
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Sorry", reg, "Ok");
                    return;
                }

            }






        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
    
}
