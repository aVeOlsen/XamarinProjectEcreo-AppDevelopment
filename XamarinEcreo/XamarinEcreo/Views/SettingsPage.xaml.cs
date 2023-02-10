using EcreoLibraryStandard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using XamarinEcreo.ViewModels;

namespace XamarinEcreo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BaseID), nameof(BaseID))]
    public partial class SettingsPage : ContentPage
    {
        //public UserGetModel User { get; set; }
        
        public string BaseID { get; set; }
        public SettingsPage()
        {
            //BindingContext = new SettingsViewModel();
            //_userDbService = DependencyService.Get<IUserDbService>();
            //User = new UserGetModel();
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (SettingsViewModel)BindingContext;
            await vm.LoadPhotoCmd.ExecuteAsync();
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var location = await Geolocation.GetLastKnownLocationAsync();
        //        if (location == null)
        //        {
        //            location = await Geolocation.GetLocationAsync(new GeolocationRequest
        //            {
        //                DesiredAccuracy = GeolocationAccuracy.Default,
        //                Timeout = TimeSpan.FromSeconds(30)
        //            });
        //        }
        //        if (location == null)
        //            LabelLocation.Text = "No GPS";
        //        else
        //            LabelLocation.Text = "Latitude: " + location.Latitude.ToString() + " and Longitude: " + location.Longitude.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Something is wrong", ex.Message);
        //    }
        //}
    }
}