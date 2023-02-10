using EcreoLibraryStandard;
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
    [QueryProperty(nameof(BaseID), nameof(BaseID))]
    public partial class AddUserPage : ContentPage
    {
        public string BaseID { get; set; }
        public AddUserPage()
        {
            InitializeComponent();
        }
    }
}