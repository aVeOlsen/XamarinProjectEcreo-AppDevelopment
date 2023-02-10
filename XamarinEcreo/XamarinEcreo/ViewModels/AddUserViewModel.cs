using EcreoLibraryStandard;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using XamarinEcreo.Services;
using XamarinEcreo.Views;

namespace XamarinEcreo.ViewModels
{
    public class AddUserViewModel:BaseViewModel, IQueryAttributable
    {
        string firstName, lastName, email, phone, address, password;
        public string FirstName { get => firstName; set => SetProperty(ref firstName, value); }
        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }
        public string Email { get=> email; set=> SetProperty(ref email, value); }
        public string Phone { get=>phone; set=>SetProperty(ref phone, value); }
        public string Address { get=> address; set=>SetProperty(ref address, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        public string[] AllRoles { get; } = Enum.GetNames(typeof(Role));

        private UserDbService _userDbService;
        public AsyncCommand AddCommand { get;}
        private Role selectedRole = Role.RegularEmployee;
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                SetProperty(ref selectedRole, value);
            }
        }
        public User User { get; set; } = new User();

        public AddUserViewModel()
        {
            Title = "Add user page";
            _userDbService = new UserDbService();
            AddCommand = new AsyncCommand(Save);
        }
        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string name = HttpUtility.UrlDecode(query["BaseID"]);
            if (!string.IsNullOrEmpty(name))
            {
                User = await _userDbService.GetItemAsync(name);
            }
        }
        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                return;
            }
            await _userDbService.AddUser(firstName, lastName, false, SelectedRole, email, phone, address, password);

            //Går tilbage, lidt som cd ..   (Kan også bruge multiple layers som ../../ )
            await Shell.Current.GoToAsync("..");
        }

    }
}
