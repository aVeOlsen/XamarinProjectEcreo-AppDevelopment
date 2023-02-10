using EcreoLibraryStandard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEcreo.Services;
using XamarinEcreo.ViewModels;

namespace XamarinEcreo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BaseID), nameof(BaseID))]
    public partial class AdminPage : ContentPage
    {
        private IUserDbService _userDbService;
        public string BaseID
        {
            get
            {
                return BaseID;
            }
        }
        //public User user { get=> LoadUser();}
        public AdminPage()
        {
            InitializeComponent();
            BindingContext = new AdminViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (AdminViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
        //User LoadUser()
        //{
        //    try
        //    {
        //        var user = JsonConvert.DeserializeObject<User>(Preferences.Get("UserKey", "default_value"));
        //        return user;
        //        //BindingContext = _userDbService.GetItemAsync(user.BaseID).Id.ToString();
        //    }
        //    catch
        //    {
        //        Debug.WriteLine("Something went wront");
        //        return null;
        //    }
        //}
    }
}