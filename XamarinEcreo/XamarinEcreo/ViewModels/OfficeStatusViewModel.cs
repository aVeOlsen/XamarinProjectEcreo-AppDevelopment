using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinEcreo.Services;
using XamarinEcreo.Views;
using System.Linq;
using System.IO;
using EcreoLibraryStandard;
using System.Web;
using XamarinEcreo.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace XamarinEcreo.ViewModels
{
    public class OfficeStatusViewModel : ViewModelBase
    {
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand OpenAbsenceCmd { get; set; }
        public ObservableRangeCollection<User> Users { get; set; }
        public ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        public User user { get; set; }
        private ImageSource imageDataUrl;
        public ImageSource ImageDataUrl
        {
            get { return imageDataUrl; }
            set
            {
                imageDataUrl = value;
                OnPropertyChanged("ImageDataUrl");
            }
        }

        SettingsViewModel svm;
        IUserDbService _userDbService;
        AdminstratorService _adminstratorService;
        IImageDbService _imageDbService;
        public OfficeStatusViewModel()
        {

            _imageDbService = DependencyService.Get<IImageDbService>();
            _adminstratorService = new AdminstratorService();
            //_userDbService = new UserDbService();
            _userDbService = DependencyService.Get<IUserDbService>();//    new UserDbService();
            Users = new ObservableRangeCollection<User>();
            UsersGet = new ObservableRangeCollection<UserGetModel>();
            OpenAbsenceCmd = new AsyncCommand(OnAbsenceClicked);
            RefreshCommand = new AsyncCommand(Refresh);
            user = new User();
            svm = new SettingsViewModel();
        }

        public async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(300);
            UsersGet.Clear();
            var users = await _userDbService.GetUsersAsync();
            //for(int i = 0; i < users.Count; i++)
            foreach (var item in users)
            {
                if (item.CurrentAbsenceStatus == AbsenceStatusRole.OnSite)
                {

                    if (item.Image != null)
                    {
                        //var stream = await _photoPickerService.GetImageStreamAsync();

                        //using (var ms = new MemoryStream(item.Image))
                        //{
                        //    ms.ReadAsync(item.Image);
                        //}
                        //var stream = await item.Image;
                        //string imageDataUrl = string.Format("data:image/jpg;base64,{0}", base64Data);
                        //svm.LoadPhotoAsync(item.);

                        //Stream stream = new MemoryStream(item.Image);
                        //var imageSource = ImageSource.FromStream(() => stream);
                        //item.ImageURL = imageSource;
                        //var byteArray = Convert.FromBase64String(item.Image);
                        //Stream stream = new MemoryStream(byteArray);
                        //var imagesource = ImageSource.FromStream(() => { return stream; });

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
            if (this.user == null)
            {
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                ////await Application.Current.MainPage.Navigation.PushAsync(new AbsenceReportPage(), true);
                await App.Current.MainPage.Navigation.PushAsync(new AbsenceReportPage(), true);
                //await Shell.Current.GoToAsync($"//OfficeStatusPage/{nameof(AbsenceReportPage)}?BaseID={UserGet.BaseID}");
            }

        }

    }
}
