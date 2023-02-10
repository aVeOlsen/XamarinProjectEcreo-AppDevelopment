using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinEcreo.Models;
using XamarinEcreo.Services;

namespace XamarinEcreo.ViewModels
{
    public class ImageViewModel:ViewModelBase
    {

        public AsyncCommand<FileResult> LoadPhotoCmd { get; set; }
        public AsyncCommand UploadPhotoCmd { get; set; }
        public AsyncCommand TakePhotoCmd { get; set; }
        public FileResult ImageFile { get; set; }

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
        public ImageViewModel()
        {
            _adminstratorService = DependencyService.Get<IAdminstratorService>();
            _imageDbService = DependencyService.Get<IImageDbService>();
            user = new UserModel();
            _client = new HttpClient();
            _userDbService = DependencyService.Get<IUserDbService>();
            UserGet = new UserGetModel();
            TakePhotoCmd = new AsyncCommand(TakePhotoAsync);
            UploadPhotoCmd = new AsyncCommand(UploadPhotoAsync);
            //LoadPhotoCmd = new AsyncCommand<FileResult>(LoadPhotoAsync);
        }
        public async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                UserGet.Image = null;
                IsBusy = true;
                return;
            }
            IsNotBusy = true;
            IsBusy = false;
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            UserGet.Image = photo.FileName;

            user = await _userDbService.GetUserAsync(UserGet.BaseID);
            user.Image = UserGet.Image;

            user.PersonalInformation = await _adminstratorService.GetUserInfoAsync(UserGet.BaseID);
            
            string tag = "XamarinEcreo.Android";
            Android.Util.Log.WriteLine(Android.Util.LogPriority.Info, tag, user.PersonalInformation.Email+": Have taken or uploadad imagefile: "+photo.FileName);



            await _userDbService.UpdateUserModelAsync(user);


            //await _imageDbService.GetImage(user.Image);
            if (UserGet.Image == null)
            {
                UserGet.Image = null;
                return;
            }
            var stream = await _imageDbService.GetImage(UserGet.Image);
            ImageURL = ImageSource.FromStream(() =>
            {
                return new MemoryStream(stream);
            });

            //await _userDbService.UpdateUserModelAsync(UserGet);
            //PhotoPath = newFile;
            //using (var stream = await photo.OpenReadAsync())
            //using (var newStream = File.OpenWrite(newFile))
            //{
            //    await stream.CopyToAsync(newStream);
            //}

            //return PhotoPath;
        }
        public async Task TakePhotoAsync()
        {
            var file = await MediaPicker.CapturePhotoAsync();

            if (file == null)
                return;
            
            await _imageDbService.UploadImageAsync(await file.OpenReadAsync(), file.FileName);
            await LoadPhotoAsync(file);

        }

        public async Task UploadPhotoAsync()
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;
            await _imageDbService.UploadImageAsync(await file.OpenReadAsync(), file.FileName);
            await LoadPhotoAsync(file);


        }
    }
}
