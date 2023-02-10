using EcreoLibraryStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEcreo.Services;
using XamarinEcreo.ViewModels;

namespace XamarinEcreo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BaseID), nameof(BaseID))]
    public partial class OfficeStatusPage : ContentPage
    {
        public string BaseID { get; set; }
        //readonly UserDbService _userDbService;
        public OfficeStatusPage()
        {
            InitializeComponent();
            BindingContext = new OfficeStatusViewModel();
            //_userDbService = new UserDbService();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (OfficeStatusViewModel)BindingContext;
                await vm.RefreshCommand.ExecuteAsync();
        }
    }
}