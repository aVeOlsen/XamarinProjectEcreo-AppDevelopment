using Android.Content.Res;
using EcreoLibraryStandard;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using XamarinEcreo.Views;

namespace XamarinEcreo.ViewModels
{
    public class HomeStatusViewModel : ViewModelBase
    {
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand OpenAbsenceCmd { get; set; }
        public ObservableRangeCollection<User> User { get; set; }
        public ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        IUserDbService _userDbService;
        IImageDbService _imageDbService;
        public AbsenceStatus status { get; set; }
        public User user { get; set; }
        public HomeStatusViewModel()
        {
            _imageDbService = DependencyService.Get<IImageDbService>();
            _userDbService = DependencyService.Get<IUserDbService>();
            User = new ObservableRangeCollection<User>();
            UsersGet = new ObservableRangeCollection<UserGetModel>();
            OpenAbsenceCmd = new AsyncCommand(OnAbsenceClicked);
            RefreshCommand = new AsyncCommand(Refresh);
            status = new AbsenceStatus();
            user = new User();
        }
        public async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(300);
            UsersGet.Clear();
            var users = await _userDbService.GetUsersAsync();
            foreach (var item in users)
            {
                if (item.CurrentAbsenceStatus == AbsenceStatusRole.Home)
                {
                    if (item.Image != null)
                    {

                        var stream = await _imageDbService.GetImage(item.Image);
                        item.ImageURL = ImageSource.FromStream(() =>
                        {
                            return new MemoryStream(stream);
                        });

                    }
                    UsersGet.Add(item);
                }
            }
            IsBusy = false;

        }
        public async Task OnAbsenceClicked()
        {
            //await INavigation.PushAsync($"{nameof(AbsenceReportPage)}?BaseID={UserGet.BaseID}");
            if (this.user == null)
            {
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                await Shell.Current.GoToAsync($"//HomeStatusPage/{nameof(AbsenceReportPage)}?BaseID={UserGet.BaseID}");
            }
        
        }


    }
}
