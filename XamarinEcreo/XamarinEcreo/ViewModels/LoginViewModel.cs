using EcreoLibraryStandard;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinEcreo.Models;
using XamarinEcreo.Services;
using XamarinEcreo.Views;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using Xamarin.Essentials;
using Newtonsoft.Json;
using XamarinEcreo.Common;


namespace XamarinEcreo.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;
         
        string username, password;
        public string UserName { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public AsyncCommand LoginCommand { get; set; }
        public User user { get; set; }
        public UserGetModel UserGet { get; set; }
        public IValidationRule<string> UserNameValidation { get; set; } = new RequiredValidationRule();

        IUserDbService _userDbService;
        IAuthenticationService _authenticationService;
        public LoginViewModel()
        {
            _userDbService = DependencyService.Get<IUserDbService>(); //new UserDbService();
            _authenticationService = new AuthenticationService(_factory, _config);
            LoginCommand = new AsyncCommand(Login);
            user = new User();
            user.PersonalInformation = new ContactInformation();
        }
        public async Task Login()
        {
            
            IsBusy = true;
            if (UserName != null && Password!=null)
            {
                var result = await _authenticationService.SignIn(UserName, Password);

                if(result == null)
                {
                    await Application.Current.MainPage.DisplayAlert("FORKERT!", "Indtastede brugernavn eller adgangskode var forkert", "ok");
                    IsBusy = false;
                    return;
                }

                if (result.Identity.IsAuthenticated == true)
                {
                    var getLastLocation = await Geolocation.GetLastKnownLocationAsync();
                    var setNewLocation = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Default,
                        Timeout = TimeSpan.FromSeconds(5)
                    });


                    if (getLastLocation == null)
                    {
                        getLastLocation = setNewLocation;
                        Preferences.Set("DateKey", DateTime.Today, "SecurityDateID");
                    }

                    if(getLastLocation != null && setNewLocation != null)
                    {
                        var gpsSecurity = Location.CalculateDistance(getLastLocation, setNewLocation, DistanceUnits.Kilometers);
                        if(gpsSecurity != 0)
                        {
                            var securityTime = TimeSpan.FromDays(2);
                            //DateTime.Today.AddDays(2);
                            var dateData = JsonConvert.DeserializeObject<DateTime>(Preferences.Get("DateKey", "default_value", "SecurityDateID"));
                            var today = DateTime.Today;
                            if(dateData +securityTime >=today && gpsSecurity>1000)
                            {
                                var timeZ = dateData + securityTime;
                                await Application.Current.MainPage.DisplayAlert("GPS authorizing failed", "An unexpected threat, against our security accoured. Contact Admin to get access.", "ok");
                                IsBusy = false;
                                return;
                            }
                        }

                    }
                    if (getLastLocation != null)
                    {
                        Preferences.Set("DateKey", JsonConvert.SerializeObject(DateTime.Today), "SecurityDateID");
                    }
                    else
                    {
                        
                        await Application.Current.MainPage.DisplayAlert("GPS authorizing failed", "Af sikkerhedsmæssige grunde, beder vi dig aktivere GPS'en, så vi bedre kan sikre os mod angreb", "ok");
                    }
                    var tmp = result.Identities.ToList();
                    var tmp2 = tmp[0].Claims.ElementAtOrDefault(1);
                    user.BaseID = tmp2.Value;
                    user = await _userDbService.GetItemAsync(user.BaseID);
                    
                    
                    Preferences.Set("UserKey", JsonConvert.SerializeObject(user), "LoginID");
                    Application.Current.Properties["id"] = user.BaseID;
                    MessagingCenter.Send<LoginViewModel>(this, (user.AdminStatus == true) ? "admin" : "user");


                    IsBusy = false;                    
                    UserName = "";
                    Password = "";
                    var route = $"//{nameof(OfficeStatusPage)}?BaseID={user.BaseID}";
                    //_logger.LogInformation(tag, UserName+": Have logged in");
                    await Shell.Current.GoToAsync(route);
                }
                else
                    await Application.Current.MainPage.DisplayAlert("FORKERT!", "Indtastede brugernavn eller adgangskode var forkert", "ok");
                IsBusy = false;
                return;
            }
            else
                await Application.Current.MainPage.DisplayAlert("FORKERT!", "Indtastede brugernavn eller adgangskode var forkert", "ok");
            IsBusy = false;
            return;
        }


    }
}
