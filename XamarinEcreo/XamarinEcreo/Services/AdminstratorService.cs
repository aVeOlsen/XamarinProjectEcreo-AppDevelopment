using EcreoLibraryStandard;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinEcreo.Models;

namespace XamarinEcreo.Services
{
    public class AdminstratorService : BaseViewModel, IAdminstratorService
    {

        static HttpClient _client;
        //static JsonSerializerOptions serializerOptions;
        private ObservableRangeCollection<User> Users { get; set; }
        private ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        private ObservableRangeCollection<ContactInformation> ContactInfo { get; set; }
        public AdminstratorService()
        {
            _client = new HttpClient();
        }
        public async Task<UserPersonalInfoModel> GetUserInfoAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorGetUserInfoRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var contactInfo = JsonConvert.DeserializeObject<UserPersonalInfoModel>(content);
            return contactInfo;
        }
        public async Task<UserPersonalInfoModel> GetUserPasswordAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorGetUserPasswordRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var passwordInfo = JsonConvert.DeserializeObject<UserPersonalInfoModel>(content);
            return passwordInfo;
        }
        public async Task<UserGetModel> GetUserRoleAsync(string id)
        {
            Uri uri = new Uri(String.Format(Constants.AdminstratorGetUserInfoRestUrl, id));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Something went wrong");
            }
            var content = await response.Content.ReadAsStringAsync();
            var roleInfo = JsonConvert.DeserializeObject<UserGetModel>(content);
            return roleInfo;
        }

    }
}
