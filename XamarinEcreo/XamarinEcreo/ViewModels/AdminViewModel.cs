using System;
using System.Collections.Generic;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinEcreo.Services;
using EcreoLibraryStandard;
using XamarinEcreo.Views;
using System.Web;
using XamarinEcreo.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace XamarinEcreo.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        public ObservableRangeCollection<UserGetModel> UsersGet { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand AddCommand { get; set; }
        public AsyncCommand<UserGetModel> RemoveCommand { get; set; }
        public AsyncCommand<User> UpdateCommand { get; set; }
        IUserDbService _userDbService;
        public string[] AllRoles { get; } = Enum.GetNames(typeof(Role));

        private Role selectedRole = Role.RegularEmployee;
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                SetProperty(ref selectedRole, value);
            }
        }
        public User user { get; set; }

        public AdminViewModel()
        {
            Title = "Admin Page";
            UsersGet = new ObservableRangeCollection<UserGetModel>();
            _userDbService = DependencyService.Get<IUserDbService>();
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<UserGetModel>(Remove);
            UpdateCommand = new AsyncCommand<User>(Update);
            user = new User();
        }


        async Task Add()
        {
            var route = $"{nameof(AddUserPage)}?BaseID={UserGet.BaseID}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Update(User user)
        {
            await _userDbService.UpdateUserAsync(user);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(300);
            UsersGet.Clear();
            var user = await _userDbService.GetUsersAsync();
            UsersGet.AddRange(user);
            IsBusy = false;
        }
        async Task Remove(UserGetModel user)
        {
            await _userDbService.RemoveUser(user.BaseID);
            await Refresh();
        }

    }
}
