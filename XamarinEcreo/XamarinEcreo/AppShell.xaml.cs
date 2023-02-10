using EcreoLibraryStandard;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinEcreo.Services;
using XamarinEcreo.ViewModels;
using XamarinEcreo.Views;

namespace XamarinEcreo
{
    //[QueryProperty(nameof(User), nameof(BaseID))]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public IDictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            //BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add(nameof(AddUserPage), typeof(AddUserPage));
            Routes.Add(nameof(RegistrationPage), typeof(RegistrationPage));
            Routes.Add("HomeStatusPage/AbsenceReportPage", typeof(AbsenceReportPage));
            Routes.Add($"SettingsPage/ImagePage", typeof(ImagePage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private void SignOut_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Application.Current.Properties["id"] = null;
            Shell.Current.GoToAsync("//LoginPage");
        }
        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    base.OnNavigating(args);
        //    var user  = JsonConvert.DeserializeObject<User>(Preferences.Get("UserKey", "default_value", "LoginID"));
            
        //}

    }
}
