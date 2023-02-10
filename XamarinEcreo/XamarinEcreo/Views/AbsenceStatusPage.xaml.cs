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
    public partial class AbsenceStatusPage : ContentPage
    {
        public string BaseID { get; set; }
        public AbsenceStatusPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (AbsenceStatusViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}