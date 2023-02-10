using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinEcreo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var loggedin = false;
        //    if (loggedin == true)
        //    {
        //        await Shell.Current.GoToAsync($"//{nameof(OfficeStatusPage)}");
        //    }
        //    return;
        //}

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    //Når man bruger denne relative route, tillader vi den fjerne baggrunds stack for os automatisk
        //    await Shell.Current.GoToAsync($"//{nameof(OfficeStatusPage)}");
        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Her har vi absolute route der siger loginpage/registrationpage  Som er meget behjælpeligt, hvis man er længere henne i stacken
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegistrationPage)}");
        }
    }
}