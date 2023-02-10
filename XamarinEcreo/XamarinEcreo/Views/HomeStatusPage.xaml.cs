using EcreoLibraryStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEcreo.ViewModels;

namespace XamarinEcreo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BaseID), nameof(BaseID))]
    public partial class HomeStatusPage : ContentPage
    {
        public string BaseID { get; set; }
        public HomeStatusPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (HomeStatusViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}