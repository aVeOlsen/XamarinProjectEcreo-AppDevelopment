using EcreoLibraryStandard;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using XamarinEcreo.Views;

namespace XamarinEcreo.ViewModels
{
    public class ViewModelBase : BaseViewModel, IQueryAttributable
    {
        private IUserDbService _userDbService;
        private UserGetModel _userGet;
        public UserGetModel UserGet
        {
            get { return _userGet; }
            set
            {
                _userGet = value;
                OnPropertyChanged("UserGet");
            }
        }

        public ViewModelBase()
        {
            _userDbService = DependencyService.Get<IUserDbService>();
            UserGet = new UserGetModel();
        }
        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {

            if (query.Values.Count != 0)
            {
                string name = HttpUtility.UrlDecode(query["BaseID"]);
                if (name != "")
                {

                    UserGet = await _userDbService.GetUserModelAsync(name);
                }
                else
                {
                    if (Application.Current.Properties.ContainsKey("id"))
                    {
                        UserGet = await _userDbService.GetUserModelAsync(Application.Current.Properties["id"] as string);
                        //UserGet = await _userDbService.GetUserModelAsync(name);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Fejl!", "Noget gik galt. Prøv at logge ud og ind igen.", "ok");
                    //var tmp = JsonConvert.DeserializeObject<User>(Preferences.Get("UserKey", "default_value", "LoginID"));
                    //var id = tmp.BaseID;
                    //UserGet = await _userDbService.GetUserModelAsync(id);
                    //await Shell.Current.GoToAsync($"../OfficeStatusPage");
                }
            }
            else
            {
                if (Application.Current.Properties.ContainsKey("id"))
                {
                    UserGet = await _userDbService.GetUserModelAsync(Application.Current.Properties["id"] as string);
                    //UserGet = await _userDbService.GetUserModelAsync(name);
                }
                else
                {
                    var tmp = JsonConvert.DeserializeObject<User>(Preferences.Get("UserKey", "default_value", "LoginID"));
                    var id = tmp.BaseID;
                    UserGet = await _userDbService.GetUserModelAsync(id);
                }
            }
        }
    }
}
