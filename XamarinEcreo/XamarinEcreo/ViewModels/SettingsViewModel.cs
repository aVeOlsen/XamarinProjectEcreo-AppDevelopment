using EcreoLibraryStandard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using XamarinEcreo.Views;

namespace XamarinEcreo.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IQueryAttributable
    {

        public AsyncCommand LoadPhotoCmd { get; set; }
        public AsyncCommand UploadPhotoCmd { get; set; }


        private ImageSource photoPath;
        public ImageSource PhotoPath
        {
            get { return photoPath; }
            set
            {
                photoPath = value;
                OnPropertyChanged("PhotoPath");
            }
        }
        private ImageSource imageURL;
        public ImageSource ImageURL
        {
            get { return imageURL; }
            set
            {
                imageURL = value;
                OnPropertyChanged("ImageURL");
            }
        }

        HttpClient _client;
        UserModel user;
        IUserDbService _userDbService;
        IImageDbService _imageDbService;
        IAdminstratorService _adminstratorService;
        public SettingsViewModel()
        {

            _adminstratorService = DependencyService.Get<IAdminstratorService>();
            _imageDbService = DependencyService.Get<IImageDbService>();
            user = new UserModel();
            _client = new HttpClient();
            _userDbService = DependencyService.Get<IUserDbService>();
            UserGet = new UserGetModel();
            UploadPhotoCmd = new AsyncCommand(UploadPhotoAsync);
            LoadPhotoCmd = new AsyncCommand(LoadPhotoAsync);
        }
        public async Task LoadPhotoAsync()
        {
            var tmp = JsonConvert.DeserializeObject<User>(Preferences.Get("UserKey", "default_value", "LoginID"));
            UserGet = await _userDbService.GetUserModelAsync(tmp.BaseID);

            if (UserGet.Image == null)
            {
                UserGet.Image = null;
                IsBusy = true;
                return;
            }
            var stream = await _imageDbService.GetImage(UserGet.Image);
            ImageURL = ImageSource.FromStream(() =>
            {
                return new MemoryStream(stream);
            });

            //return UserGet.ImageURL;

            //return PhotoPath;

            //var file = await photo.OpenReadAsync();
            //if (file == null)
            //{
            //    return;
            //}
            //PhotoPath = ImageSource.FromStream(() => file);
        }

        public async Task UploadPhotoAsync(/*object sender, EventArgs e*/)
        {
            //var file = await MediaPicker.PickPhotoAsync();

            //if (file == null)
            //    return;
            ////Virker til at vise billede
            ////var stream = await file.OpenReadAsync();
            ////PhotoPath = ImageSource.FromStream(() => stream);
            ////UserGet.Image = PhotoPath.ToString();
            ////var content = new MultipartFormDataContent();

            ////Virker også.
            ////Uri uri = new Uri(String.Format(Constants.ImageRestUrl, file.FileName));
            ////content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);
            ////var response = await _client.PostAsync(uri, content);
            ////if (!response.IsSuccessStatusCode)
            ////{
            ////    Debug.WriteLine(response);
            ////}
            //await _imageDbService.UploadImageAsync(await file.OpenReadAsync(), file.FileName);
            ////await LoadPhotoAsync(file);
            if (UserGet == null)
            {
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                await Shell.Current.GoToAsync($"//SettingsPage/{nameof(ImagePage)}?BaseID={UserGet.BaseID}");
            }


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
