using EcreoLibraryStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using XamarinEcreo.ViewModels;

namespace XamarinEcreo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BaseID), nameof(BaseID))]
    public partial class AbsenceReportPage : ContentPage
    {
        public string BaseID { get; set; }
        private AbsenceReportViewModel arvm;
        private UserGetModel User { get; set; }
        //private string Firstname { get => User.Firstname; }

        public AbsenceReportPage()
        {
            InitializeComponent();
            arvm = new AbsenceReportViewModel();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    BindingContext = await _userDbService.GetItemAsync(user.BaseID);
        //}

    }
}