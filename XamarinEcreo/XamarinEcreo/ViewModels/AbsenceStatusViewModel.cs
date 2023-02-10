using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using EcreoLibraryStandard;
using XamarinEcreo.Services;
using XamarinEcreo.Views;
using XamarinEcreo.Models;
using System.IO;

namespace XamarinEcreo.ViewModels
{
    public class AbsenceStatusViewModel:ViewModelBase
    {
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand OpenAbsenceCmd { get; set; }
        public ObservableRangeCollection<User> User { get; set; }
        public ObservableRangeCollection<UserGetModel> UsersGet { get; set; }

        private IImageDbService _imageDbService;
        private IUserDbService _userDbService;
        public AbsenceStatus status { get; set; }
        public User user { get; set; }
        public AbsenceStatusViewModel()
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
                if (item.CurrentAbsenceStatus == AbsenceStatusRole.Sick)
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
                await Shell.Current.GoToAsync($"//{nameof(AbsenceStatusPage)}/{nameof(AbsenceReportPage)}?BaseID={UserGet.BaseID}");
            }

        }

    }
}
